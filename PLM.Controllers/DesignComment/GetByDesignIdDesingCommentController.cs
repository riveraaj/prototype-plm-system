namespace PLM.Controllers.DesignComment;
public class GetByDesignIdDesingCommentController(IGetByParamsInputPortFactory getInputPort,
                                             IOutputPort outputPort)
{
    private readonly IGetByParamsInputPortFactory _getInputPort = getInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> GetByDesignId(int id)
    {
        var getInputPort = _getInputPort.GetInputPort<uint>();
        await getInputPort.GetByParams((uint)id);
        return _outputPort.OperationResponse;
    }
}