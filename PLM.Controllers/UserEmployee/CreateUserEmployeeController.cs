namespace PLM.Controllers.UserEmployee;
public class CreateUserEmployeeController(ICreateInputPortFactory createInputPort,
                                            IOutputPort outputPort)
{
    private readonly ICreateInputPortFactory _createInputPort = createInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Create(CreateUserEmployeeDTO oCreateUserEmployeeDTO)
    {
        var createInputPort = _createInputPort.GetInputPort<CreateUserEmployeeDTO>();
        await createInputPort.Create(oCreateUserEmployeeDTO);
        return _outputPort.OperationResponse;
    }
}