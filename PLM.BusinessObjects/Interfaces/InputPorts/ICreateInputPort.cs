namespace PLM.BusinessObjects.Interfaces.InputPorts;
public interface ICreateInputPort<T>
{
    public Task Create(T entity);
}