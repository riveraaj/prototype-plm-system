namespace PLM.Controllers.ProductProposal;
public class GetAllProductProposalController(IGetAllInputPortFactory getAllInputPort,
                                  IOutputPort outputPort)
{
    private readonly IGetAllInputPortFactory _getAllInputPort = getAllInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> GetAll(Filter oFilter)
    {
        var getAllInputPort = _getAllInputPort.GetInputPort(InputPortType.ProductProposal);
        await getAllInputPort.GetAll(oFilter);
        return _outputPort.OperationResponse;
    }
}