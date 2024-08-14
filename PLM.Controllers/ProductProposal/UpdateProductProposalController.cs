namespace PLM.Controllers.ProductProposal;
public class UpdateProductProposalController(IUpdateInputPortFactory updateInputPort,
                                            IOutputPort outputPort)
{
    private readonly IUpdateInputPortFactory _updateInputPort = updateInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Update(UpdateProductProposalDTO oUpdateProductProposalDTO)
    {
        var updateInputPort = _updateInputPort.GetInputPort<UpdateProductProposalDTO>();
        await updateInputPort.Update(oUpdateProductProposalDTO);
        return _outputPort.OperationResponse;
    }
}