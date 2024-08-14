namespace PLM.BusinessObjects.Interfaces.InputPorts;
public interface IUpdateInputPort<T>
{
    public Task Update(T entity);
}