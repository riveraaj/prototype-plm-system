namespace PLM.Controllers.UserEmployee;
public class GetByIdUserEmployeeController(IGetByParamsInputPortFactory getInputPort,
                                           IOutputPort outputPort)
{
    private readonly IGetByParamsInputPortFactory _getInputPort = getInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Get(int id)
    {
        var getInputPort = _getInputPort.GetInputPort<int>();
        await getInputPort.GetByParams(id);
        return _outputPort.OperationResponse;
    }
}