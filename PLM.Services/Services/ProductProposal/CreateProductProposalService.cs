namespace PLM.BusinessLogic.Services.ProductProposal;
/// <summary>
/// Service for creating a product proposal.
/// Implements the ICreateInputPort interface to handle creation operations.
/// <param name="productProposalRepository">Product proposal repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// <param name="fileConverterFactory">File converter factory to handle file conversion</param>
/// </summary>
internal class CreateProductProposalService(IProductProposalRepository productProposalRepository,
                                            IOutputPort outputPort,
                                            IFileConverterFactory fileConverterFactory)
    : ICreateInputPort<CreateProductProposalDTO>
{
    private readonly IProductProposalRepository _productProposalRepository = productProposalRepository;
    private readonly IOutputPort _outputPort = outputPort;
    private readonly IFileConverterFactory _fileConverterFactory = fileConverterFactory;

    /// <summary>
    /// Method to create a product proposal based on the provided DTO.
    /// Handles file processing and validation before saving the proposal.
    /// </summary>
    /// <param name="oCreateProductProposalDTO">DTO containing data for creating a product proposal</param>
    public async Task Create(CreateProductProposalDTO oCreateProductProposalDTO)
    {
        try
        {
            oCreateProductProposalDTO.FileUploadDTO.Name = "ProductProposalDocument";

            // Handle file processing and set file path
            oCreateProductProposalDTO.FilePath = HandlerFileHelper.HandleFile(oCreateProductProposalDTO.IdeaId,
                                                            oCreateProductProposalDTO.UserEmployeeId,
                                                            oCreateProductProposalDTO.FileUploadDTO,
                                                            _fileConverterFactory);

            // Validate the DTO
            var validationResult = oCreateProductProposalDTO.ValidateModel();
            object response;

            if (validationResult.IsValid)
            {
                var ProductProposal = ProductProposalMapper.MapProductProposal(oCreateProductProposalDTO);
                response = await _productProposalRepository.CreateAsync(ProductProposal);
            }
            else
                response = new OperationResponse()
                {
                    Code = -4,
                    Message = "Errores de validación en los datos enviados.",
                    Content = validationResult.ErrorMessages.Select(msg => (object)msg!).ToList()
                };

            await _outputPort.Handle((OperationResponse)response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            await _outputPort.Handle(new OperationResponse
            {
                Code = -3,
                Message = "Se ha producido un problema, por favor inténtelo de nuevo. " +
                          "Si el problema persiste, póngase en contacto con el servicio técnico",
                Content = []
            });
        }
    }
}