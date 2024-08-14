namespace PLM.Controllers.ReviewProductProposal;
public class CreateReviewProductProposalController(ICreateInputPortFactory createInputPort,
                                                     IOutputPort outputPort)
{
    private readonly ICreateInputPortFactory _createInputPort = createInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Create(CreateReviewProductProposalDTO oCreateReviewProductProposalDTO)
    {
        var createInputPort = _createInputPort.GetInputPort<CreateReviewProductProposalDTO>();
        await createInputPort.Create(oCreateReviewProductProposalDTO);
        return _outputPort.OperationResponse;
    }
}