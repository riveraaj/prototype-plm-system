namespace PLM.Controllers.Design;
public class CreateDesignController(ICreateInputPortFactory createInputPort,
                                    IOutputPort outputPort)
{
    private readonly ICreateInputPortFactory _createInputPort = createInputPort;
    private readonly IOutputPort _outputPort = outputPort;

    public async Task<OperationResponse> Create(CreateDesignDTO oCreateDesignDTO)
    {
        var createInputPort = _createInputPort.GetInputPort<CreateDesignDTO>();
        await createInputPort.Create(oCreateDesignDTO);
        return _outputPort.OperationResponse;
    }
}