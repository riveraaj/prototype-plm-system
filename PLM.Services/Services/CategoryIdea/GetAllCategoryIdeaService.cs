namespace PLM.BusinessLogic.Services.CategoryIdea;
/// <summary>
/// Service for retrieving all category ideas.
/// Implements the IGetAllInputPort interface to handle retrieval operations.
/// <param name="categoryIdeaRepository">Repository for category idea data operations</param>
/// <param name="outputPort">Output port to manage responses</param>
/// </summary>
internal class GetAllCategoryIdeaService(ICategoryIdeaRepository categoryIdeaRepository,
                                         IOutputPort outputPort)
    : IGetAllInputPort
{
    private readonly ICategoryIdeaRepository _categoryIdeaRepository = categoryIdeaRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to retrieve all category ideas based on the provided filter.
    /// </summary>
    /// <param name="oFilter">Filter object containing filter criteria</param>
    public async Task GetAll(Filter oFilter)
    {
        try
        {
            var response = await _categoryIdeaRepository.GetAllAsync();
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