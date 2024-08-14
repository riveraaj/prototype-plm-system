namespace PLM.BusinessObjects.Interfaces.InputPorts;
public interface IGetAllInputPort
{
    public Task GetAll(Filter oFilter);
}