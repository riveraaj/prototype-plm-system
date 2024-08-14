namespace PLM.BusinessLogic.Services.Idea;
/// <summary>
/// Service for updating an idea.
/// Implements the IUpdateInputPort interface to handle update operations.
/// </summary>
/// <param name="ideaRepository">Repository for idea data operations</param>
/// <param name="outputPort">Output port to manage responses</param>
internal class UpdateIdeaService(IIdeaRepository ideaRepository,
                                 IOutputPort outputPort) : IUpdateInputPort<UpdateIdeaDTO>
{
    private readonly IIdeaRepository _ideaRepository = ideaRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to update an idea based on the provided DTO.
    /// Validates the DTO before performing the update operation.
    /// </summary>
    /// <param name="oUpdateIdeaDTO">DTO containing data for updating an idea</param>
    public async Task Update(UpdateIdeaDTO oUpdateIdeaDTO)
    {
        try
        {
            // Validate the DTO
            var validationResult = oUpdateIdeaDTO.ValidateModel();
            object response;

            if (validationResult.IsValid)
            {
                var idea = IdeaMapper.MapIdea(oUpdateIdeaDTO);
                response = await _ideaRepository.UpdateAsync(idea);
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