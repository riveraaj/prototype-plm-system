namespace PLM.Controllers.Design;
public class UpdateDesignController(IUpdateInputPortFactory updateInputPort,
                                    IOutputPort outputPort)
{
    private readonly IUpdateInputPortFactory _updateInputPort = updateInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Update(UpdateDesignDTO oUpdateDesignDTO)
    {
        var updateInputPort = _updateInputPort.GetInputPort<UpdateDesignDTO>();
        await updateInputPort.Update(oUpdateDesignDTO);
        return _outputPort.OperationResponse;
    }
}