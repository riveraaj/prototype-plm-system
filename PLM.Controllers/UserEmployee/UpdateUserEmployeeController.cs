namespace PLM.Controllers.UserEmployee;
public class UpdateUserEmployeeController(IUpdateInputPortFactory updateInputPort,
                                  IOutputPort outputPort)
{
    private readonly IUpdateInputPortFactory _updateInputPort = updateInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Update(UpdateUserEmployeeDTO oUpdateUserEmployeeDTO)
    {
        var updateInputPort = _updateInputPort.GetInputPort<UpdateUserEmployeeDTO>();
        await updateInputPort.Update(oUpdateUserEmployeeDTO);
        return _outputPort.OperationResponse;
    }
}