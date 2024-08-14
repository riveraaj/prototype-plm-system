namespace PLM.BusinessLogic.Services.UserEmployee;
/// <summary>
/// Service for retrieving all user employee information with filters.
/// Implements the IGetAllInputPort interface to handle retrieval with filtering.
/// <param name="userEmployeeRepository">User employee repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// </summary>
internal class GetAllUserEmployeeService(IUserEmployeeRepository userEmployeeRepository,
                                         IOutputPort outputPort)
    : IGetAllInputPort
{
    private readonly IUserEmployeeRepository _userEmployeeRepository = userEmployeeRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to retrieve all user employee information with the specified filters.
    /// </summary>
    /// <param name="oFilter">Filter object containing the parameters for retrieval</param>
    public async Task GetAll(Filter oFilter)
    {
        try
        {
            var response = await _userEmployeeRepository.GetAllAsync(oFilter.ParamOne, oFilter.ParamTwo);
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