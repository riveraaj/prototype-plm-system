namespace PLM.BusinessObjects.Interfaces.OutputPorts;
public interface IOutputPort
{
    public OperationResponse OperationResponse { get; }
    public Task Handle(OperationResponse oOperationResponse);
}