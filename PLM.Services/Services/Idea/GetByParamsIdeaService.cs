namespace PLM.BusinessLogic.Services.Idea;
/// <summary>
/// Service for retrieving ideas based on specific parameters.
/// Implements the IGetByParamsInputPort interface to handle retrieval operations.
/// <param name="ideaRepository">Repository for idea data operations</param>
/// <param name="outputPort">Output port to manage responses</param>
/// </summary>
internal class GetByParamsIdeaService(IIdeaRepository ideaRepository,
                                      IOutputPort outputPort) : IGetByParamsInputPort<string>
{
    private readonly IIdeaRepository _ideaRepository = ideaRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to retrieve ideas based on the provided parameter.
    /// </summary>
    /// <param name="param">Parameter used to filter the ideas</param>
    public async Task GetByParams(string param)
    {
        try
        {
            var response = await _ideaRepository.GetAllForProductProposalAsync();
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