namespace PLM.BusinessLogic.Services.Design;
/// <summary>
/// Service for retrieving all design information based on provided filters.
/// Implements the IGetAllInputPort interface to handle retrieval operations.
/// <param name="designRepository">Repository for design data operations</param>
/// <param name="outputPort">Output port to manage responses</param>
/// </summary>
internal class GetAllDesignService(IDesignRepository designRepository,
                                   IOutputPort outputPort) : IGetAllInputPort
{
    private readonly IDesignRepository _designRepository = designRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to retrieve all design information based on the provided filter.
    /// </summary>
    /// <param name="oFilter">Filter containing parameters for the design retrieval</param>
    public async Task GetAll(Filter oFilter)
    {
        try
        {
            var response = await _designRepository.GetAllAsync(oFilter.ParamOne, oFilter.ParamTwo);
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