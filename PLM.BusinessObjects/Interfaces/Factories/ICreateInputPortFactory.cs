namespace PLM.BusinessObjects.Interfaces.Factories;
public interface ICreateInputPortFactory
{
    public ICreateInputPort<T> GetInputPort<T>();
}