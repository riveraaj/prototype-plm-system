namespace PLM.Controllers.ReviewProductProposal;
public class GetForDesignController(IGetByParamsInputPortFactory getInputPort,
                                    IOutputPort outputPort)
{
    private readonly IGetByParamsInputPortFactory _getInputPort = getInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Get()
    {
        var getInputPort = _getInputPort.GetInputPort<bool>();
        await getInputPort.GetByParams(true);
        return _outputPort.OperationResponse;
    }
}