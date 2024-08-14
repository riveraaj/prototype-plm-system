namespace PLM.Controllers.Idea;
public class CreateIdeaController(ICreateInputPortFactory createInputPort,
                                  IOutputPort outputPort)
{
    private readonly ICreateInputPortFactory _createInputPort = createInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Create(CreateIdeaDTO oCreateIdeaDTO)
    {
        var createInputPort = _createInputPort.GetInputPort<CreateIdeaDTO>();
        await createInputPort.Create(oCreateIdeaDTO);
        return _outputPort.OperationResponse;
    }
}