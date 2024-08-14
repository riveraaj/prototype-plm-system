namespace PLM.BusinessLogic.Services.DesignComment;
/// <summary>
/// Service for creating a design comment.
/// Implements the ICreateInputPort interface to handle creation operations.
/// <param name="designCommentRepository">Repository for design comment data operations</param>
/// <param name="outputPort">Output port to manage responses</param>
/// </summary>
internal class CreateDesignCommentService(IDesignCommentRepository designCommentRepository,
                                 IOutputPort outputPort) : ICreateInputPort<CreateDesignCommentDTO>
{
    private readonly IDesignCommentRepository _designCommentRepository = designCommentRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to create a new design comment based on the provided data.
    /// </summary>
    /// <param name="oCreateDesignCommentDTO">Data Transfer Object containing the design comment details</param>
    public async Task Create(CreateDesignCommentDTO oCreateDesignCommentDTO)
    {
        try
        {
            // Validate the provided DTO
            var validationResult = oCreateDesignCommentDTO.ValidateModel();
            object response;

            if (validationResult.IsValid)
            {
                var designComment = DesignCommentMapper.MapDesignComment(oCreateDesignCommentDTO);
                response = await _designCommentRepository.CreateAsync(designComment);
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