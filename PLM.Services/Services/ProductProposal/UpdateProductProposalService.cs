namespace PLM.BusinessLogic.Services.ProductProposal;
/// <summary>
/// Service for updating a product proposal.
/// Implements the IUpdateInputPort interface to handle updates with DTO.
/// <param name="productProposalRepository">Product proposal repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// <param name="fileConverterFactory">File converter factory</param>
/// </summary>
internal class UpdateProductProposalService(IProductProposalRepository productProposalRepository,
                                            IOutputPort outputPort,
                                            IFileConverterFactory fileConverterFactory)
    : IUpdateInputPort<UpdateProductProposalDTO>
{
    private readonly IProductProposalRepository _productProposalRepository = productProposalRepository;
    private readonly IOutputPort _outputPort = outputPort;
    private readonly IFileConverterFactory _fileConverterFactory = fileConverterFactory;

    /// <summary>
    /// Method to update a product proposal with the provided DTO.
    /// Handles file processing and validation before updating the product proposal.
    /// </summary>
    /// <param name="oUpdateProductProposalDTO">DTO with product proposal update data</param>
    public async Task Update(UpdateProductProposalDTO oUpdateProductProposalDTO)
    {
        try
        {
            oUpdateProductProposalDTO.FileUploadDTO.Name = "ProductProposalDocument";

            // Process and handle the file associated with the product proposal
            oUpdateProductProposalDTO.FilePath = HandlerFileHelper.HandleFile(oUpdateProductProposalDTO.Id,
                                                            oUpdateProductProposalDTO.UserEmployeeId,
                                                            oUpdateProductProposalDTO.FileUploadDTO,
                                                            _fileConverterFactory);
            // Validate the DTO model
            var validationResult = oUpdateProductProposalDTO.ValidateModel();
            object response;

            if (validationResult.IsValid)
            {
                var ProductProposal = ProductProposalMapper.MapProductProposal(oUpdateProductProposalDTO);
                response = await _productProposalRepository.UpdateAsync(ProductProposal);
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