namespace PLM.Controllers.Design;
public class DeleteDesignController(IDeleteInputPortFactory deleteInputPort,
                                    IOutputPort outputPort)
{
    private readonly IDeleteInputPortFactory _deleteInputPort = deleteInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Delete(int id)
    {
        var deleteInputPort = _deleteInputPort.GetInputPort(InputPortType.Design);
        await deleteInputPort.Delete(id);
        return _outputPort.OperationResponse;
    }
}