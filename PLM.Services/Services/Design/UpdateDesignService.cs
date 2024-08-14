namespace PLM.BusinessLogic.Services.Design;
/// <summary>
/// Service for updating design information.
/// Implements the IUpdateInputPort interface to handle update operations.
/// <param name="designRepository">Repository for design data operations</param>
/// <param name="outputPort">Output port to manage responses</param>
/// <param name="fileConverterFactory">Factory for creating file converters</param>
/// </summary>
internal class UpdateDesignService(IDesignRepository designRepository,
                                   IOutputPort outputPort,
                                   IFileConverterFactory fileConverterFactory)
    : IUpdateInputPort<UpdateDesignDTO>
{
    private readonly IDesignRepository _designRepository = designRepository;
    private readonly IOutputPort _outputPort = outputPort;
    private readonly IFileConverterFactory _fileConverterFactory = fileConverterFactory;

    /// <summary>
    /// Method to update a design based on the provided design data transfer object (DTO).
    /// </summary>
    /// <param name="oUpdateDesignDTO">DTO containing the data to update the design</param>
    public async Task Update(UpdateDesignDTO oUpdateDesignDTO)
    {
        try
        {
            oUpdateDesignDTO.FileUploadDTO.Name = "DesignDocument";

            // Handle file processing and assign the file path to the DTO
            oUpdateDesignDTO.CurrentDesignPath = HandlerFileHelper.HandleFile(oUpdateDesignDTO.Id,
                                                            oUpdateDesignDTO.UserEmployeeId,
                                                            oUpdateDesignDTO.FileUploadDTO,
                                                            _fileConverterFactory);

            // Validate the DTO
            var validationResult = oUpdateDesignDTO.ValidateModel();
            object response;

            if (validationResult.IsValid)
            {
                var Design = DesignMapper.MapDesign(oUpdateDesignDTO);
                response = await _designRepository.UpdateAsync(Design);
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