namespace PLM.BusinessObjects.Interfaces.Factories;
public interface IUpdateInputPortFactory
{
    public IUpdateInputPort<T> GetInputPort<T>();
}