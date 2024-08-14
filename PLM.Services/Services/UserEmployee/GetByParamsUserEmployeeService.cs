namespace PLM.BusinessLogic.Services.UserEmployee;
/// <summary>
/// Service for retrieving user employee information by parameters.
/// Implements the IGetByParamsInputPort interface to handle retrieval by ID.
/// <param name="userEmployeeRepository">User employee repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// </summary>
internal class GetByParamsUserEmployeeService(IUserEmployeeRepository userEmployeeRepository,
                                              IOutputPort outputPort) :
    IGetByParamsInputPort<int>
{
    private readonly IUserEmployeeRepository _userEmployeeRepository = userEmployeeRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to retrieve user employee information by ID.
    /// </summary>
    /// <param name="id">ID of the user employee to retrieve</param>
    public async Task GetByParams(int id)
    {
        try
        {
            var response = await _userEmployeeRepository.GetByIdAsync(id);
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