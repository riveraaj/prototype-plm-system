namespace PLM.BusinessLogic.Services.Design;
/// <summary>
/// Service for creating a design based on the provided design data.
/// Implements the ICreateInputPort interface to handle create operations.
/// <param name="designRepository">Repository for design data operations</param>
/// <param name="outputPort">Output port to manage responses</param>
/// <param name="fileConverterFactory">Factory for file conversions</param>
/// </summary>
internal class CreateDesignService(IDesignRepository designRepository,
                                   IOutputPort outputPort,
                                   IFileConverterFactory fileConverterFactory)
    : ICreateInputPort<CreateDesignDTO>
{
    private readonly IDesignRepository _designRepository = designRepository;
    private readonly IOutputPort _outputPort = outputPort;
    private readonly IFileConverterFactory _fileConverterFactory = fileConverterFactory;

    /// <summary>
    /// Method to create a new design based on the provided design data.
    /// </summary>
    /// <param name="oCreateDesignDTO">Data transfer object containing design details</param>
    public async Task Create(CreateDesignDTO oCreateDesignDTO)
    {
        try
        {
            oCreateDesignDTO.FileUploadDTO.Name = "DesignDocument";

            // Handle the file upload and set the file path in the DTO
            oCreateDesignDTO.CurrentDesignPath = HandlerFileHelper.HandleFile(oCreateDesignDTO.ReviewProductProposalId,
                                                            oCreateDesignDTO.UserEmployeeId,
                                                            oCreateDesignDTO.FileUploadDTO,
                                                            _fileConverterFactory);
            // Validate the model
            var validationResult = oCreateDesignDTO.ValidateModel();
            object response;

            if (validationResult.IsValid)
            {
                var Design = DesignMapper.MapDesign(oCreateDesignDTO);
                response = await _designRepository.CreateAsync(Design);
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