namespace PLM.BusinessLogic.Services.UserEmployee;
/// <summary>
/// Service for deleting a user employee by ID.
/// Implements the IDeleteInputPort interface to handle deletion by ID.
/// <param name="userEmployeeRepository">User employee repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// </summary>
internal class DeleteUserEmployeeService(IUserEmployeeRepository userEmployeeRepository,
                                         IOutputPort outputPort)
    : IDeleteInputPort
{
    private readonly IUserEmployeeRepository _userEmployeeRepository = userEmployeeRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to delete a user employee by ID.
    /// </summary>
    /// <param name="id">ID of the user employee to delete</param>
    public async Task Delete(int id)
    {
        try
        {
            object response;

            if (id > 0) response = await _userEmployeeRepository.DeleteAsync(id);
            else response = new OperationResponse
            {
                Code = -4,
                Message = "Errores de validación en los datos enviados.",
                Content = ["El id es un campo obligatorio"]
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