namespace PLM.BusinessObjects.Interfaces.Factories;
public interface IGetByParamsInputPortFactory
{
    public IGetByParamsInputPort<T> GetInputPort<T>();
}