namespace PLM.BusinessLogic.Services.Idea;
/// <summary>
/// Service for retrieving all ideas based on filters.
/// Implements the IGetAllInputPort interface to handle retrieval operations.
/// <param name="ideaRepository">Repository for idea data operations</param>
/// <param name="outputPort">Output port to manage responses</param>
/// </summary>
public class GetAllIdeaService(IIdeaRepository ideaRepository,
                               IOutputPort outputPort) : IGetAllInputPort
{
    private readonly IIdeaRepository _ideaRepository = ideaRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to retrieve all ideas based on the provided filters.
    /// </summary>
    /// <param name="oFilter">Filter object containing parameters for the retrieval</param>
    public async Task GetAll(Filter oFilter)
    {
        try
        {
            var response = await _ideaRepository.GetAllAsync(oFilter.ParamOne, oFilter.ParamTwo);
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