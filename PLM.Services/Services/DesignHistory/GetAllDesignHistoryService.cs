namespace PLM.BusinessLogic.Services.DesignHistory;
/// <summary>
/// Service for retrieving all design history records.
/// Implements the IGetAllInputPort interface to handle retrieval operations.
/// <param name="designHistoryRepository">Repository for design history data operations</param>
/// <param name="outputPort">Output port to manage responses</param>
/// </summary>
internal class GetAllDesignHistoryService(IDesignHistoryRepository designHistoryRepository,
                                   IOutputPort outputPort) : IGetAllInputPort
{
    private readonly IDesignHistoryRepository _designHistoryRepository = designHistoryRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to retrieve all design history records based on the provided filter.
    /// </summary>
    /// <param name="oFilter">Filter object containing parameters to filter the design history records</param>
    public async Task GetAll(Filter oFilter)
    {
        try
        {
            var response = await _designHistoryRepository.GetAllAsync(oFilter.ParamOne, oFilter.ParamTwo, oFilter.ParamThree);
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