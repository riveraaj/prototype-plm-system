namespace PLM.BusinessObjects.Interfaces.Repositories;
public interface IRoleRepository
{
    public Task<OperationResponse> GetAllAsync();
}