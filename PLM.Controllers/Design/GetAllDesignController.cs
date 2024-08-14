namespace PLM.Controllers.Design;
public class GetAllDesignController(IGetAllInputPortFactory getAllInputPort,
                                     IOutputPort outputPort)
{
    private readonly IGetAllInputPortFactory _getAllInputPort = getAllInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> GetAll(Filter oFilter)
    {
        var getAllInputPort = _getAllInputPort.GetInputPort(InputPortType.Design);
        await getAllInputPort.GetAll(oFilter);
        return _outputPort.OperationResponse;
    }
}