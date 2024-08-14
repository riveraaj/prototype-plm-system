namespace PLM.Controllers.Idea;
public class DeleteIdeaController(IDeleteInputPortFactory deleteInputPort,
                                  IOutputPort outputPort)
{
    private readonly IDeleteInputPortFactory _deleteInputPort = deleteInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Delete(int id)
    {
        var deleteInputPort = _deleteInputPort.GetInputPort(InputPortType.Idea);
        await deleteInputPort.Delete(id);
        return _outputPort.OperationResponse;
    }
}