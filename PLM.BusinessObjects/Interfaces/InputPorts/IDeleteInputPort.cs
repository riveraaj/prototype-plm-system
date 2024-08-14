namespace PLM.BusinessObjects.Interfaces.InputPorts;
public interface IDeleteInputPort
{
    public Task Delete(int id);
}