namespace PLM.BusinessObjects.Interfaces.InputPorts;
public interface IGetByParamsInputPort<T>
{
    public Task GetByParams(T param);
}