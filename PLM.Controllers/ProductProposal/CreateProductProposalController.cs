namespace PLM.Controllers.ProductProposal;
public class CreateProductProposalController(ICreateInputPortFactory createInputPort,
                                          IOutputPort outputPort)
{
    private readonly ICreateInputPortFactory _createInputPort = createInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Create(CreateProductProposalDTO oCreateProductProposalDTO)
    {
        var createInputPort = _createInputPort.GetInputPort<CreateProductProposalDTO>();
        await createInputPort.Create(oCreateProductProposalDTO);
        return _outputPort.OperationResponse;
    }
}