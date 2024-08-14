namespace PLM.BusinessObjects.Interfaces.Factories;
public interface IDeleteInputPortFactory
{
    public IDeleteInputPort GetInputPort(InputPortType type);
}