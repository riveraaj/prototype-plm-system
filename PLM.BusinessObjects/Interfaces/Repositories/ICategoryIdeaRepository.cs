namespace PLM.BusinessObjects.Interfaces.Repositories;
public interface ICategoryIdeaRepository
{
    public Task<OperationResponse> GetAllAsync();
}