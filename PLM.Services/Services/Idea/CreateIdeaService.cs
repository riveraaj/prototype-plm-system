namespace PLM.BusinessLogic.Services.Idea;
/// <summary>
/// Service for creating an idea.
/// Implements the ICreateInputPort interface to handle creation operations.
/// <param name="ideaRepository">Repository for idea data operations</param>
/// <param name="outputPort">Output port to manage responses</param>
/// </summary>
internal class CreateIdeaService(IIdeaRepository ideaRepository,
                                 IOutputPort outputPort) : ICreateInputPort<CreateIdeaDTO>
{
    private readonly IIdeaRepository _ideaRepository = ideaRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to create a new idea based on the provided DTO.
    /// </summary>
    /// <param name="oCreateIdeaDTO">Data transfer object containing the idea details</param>
    public async Task Create(CreateIdeaDTO oCreateIdeaDTO)
    {
        try
        {
            // Validate DTO model
            var validationResult = oCreateIdeaDTO.ValidateModel();
            object response;

            if (validationResult.IsValid)
            {
                var idea = IdeaMapper.MapIdea(oCreateIdeaDTO);
                response = await _ideaRepository.CreateAsync(idea);
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