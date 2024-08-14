namespace PLM.Controllers.ReviewDesign;
public class CreateReviewDesignController(ICreateInputPortFactory createInputPort,
                                            IOutputPort outputPort)
{
    private readonly ICreateInputPortFactory _createInputPort = createInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Create(CreateReviewDesignDTO oCreateReviewDesignDTO)
    {
        var createInputPort = _createInputPort.GetInputPort<CreateReviewDesignDTO>();
        await createInputPort.Create(oCreateReviewDesignDTO);
        return _outputPort.OperationResponse;
    }
}