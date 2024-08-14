namespace PLM.Controllers.Idea;
public class GetAllIdeaController(IGetAllInputPortFactory getAllInputPort,
                                  IOutputPort outputPort)
{
    private readonly IGetAllInputPortFactory _getAllInputPort = getAllInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> GetAll(Filter oFilter)
    {
        var getAllInputPort = _getAllInputPort.GetInputPort(InputPortType.Idea);
        await getAllInputPort.GetAll(oFilter);
        return _outputPort.OperationResponse;
    }
}