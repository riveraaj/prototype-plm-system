namespace PLM.Controllers.DesignComment;
public class CreateDesignCommentController(ICreateInputPortFactory createInputPort,
                                  IOutputPort outputPort)
{
    private readonly ICreateInputPortFactory _createInputPort = createInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Create(CreateDesignCommentDTO oCreateDesignCommentDTO)
    {
        var createInputPort = _createInputPort.GetInputPort<CreateDesignCommentDTO>();
        await createInputPort.Create(oCreateDesignCommentDTO);
        return _outputPort.OperationResponse;
    }
}