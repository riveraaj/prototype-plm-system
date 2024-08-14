namespace PLM.BusinessLogic.Services.Role;
/// <summary>
/// Service for retrieving all roles.
/// Implements the IGetAllInputPort interface to handle role retrieval.
/// <param name="roleRepository">Role repository</param>
/// <param name="outputPort">Output port to handle responses</param>
/// </summary>
internal class GetAllRoleService(IRoleRepository roleRepository,
                                 IOutputPort outputPort) : IGetAllInputPort
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IOutputPort _outputPort = outputPort;

    /// <summary>
    /// Method to retrieve all roles.
    /// </summary>
    /// <param name="oFilter">Filter object (not used in this implementation)</param>
    public async Task GetAll(Filter oFilter)
    {
        try
        {
            var response = await _roleRepository.GetAllAsync();
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