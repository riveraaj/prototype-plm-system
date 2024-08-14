namespace PLM.BusinessObjects.Interfaces.Factories;
public interface IGetAllInputPortFactory
{
    public IGetAllInputPort GetInputPort(InputPortType type);
}