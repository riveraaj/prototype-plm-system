namespace PLM.Controllers.Idea;
public class GetForProductProposalController(IGetByParamsInputPortFactory getInputPort,
                                             IOutputPort outputPort)
{
    private readonly IGetByParamsInputPortFactory _getInputPort = getInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Get()
    {
        var getInputPort = _getInputPort.GetInputPort<string>();
        await getInputPort.GetByParams("");
        return _outputPort.OperationResponse;
    }
}