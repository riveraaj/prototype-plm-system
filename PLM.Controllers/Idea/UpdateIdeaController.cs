namespace PLM.Controllers.Idea;
public class UpdateIdeaController(IUpdateInputPortFactory updateInputPort,
                                  IOutputPort outputPort)
{
    private readonly IUpdateInputPortFactory _updateInputPort = updateInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Update(UpdateIdeaDTO oUpdateIdeaDTO)
    {
        var updateInputPort = _updateInputPort.GetInputPort<UpdateIdeaDTO>();
        await updateInputPort.Update(oUpdateIdeaDTO);
        return _outputPort.OperationResponse;
    }
}