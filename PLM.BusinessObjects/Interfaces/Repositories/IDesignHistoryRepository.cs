namespace PLM.BusinessObjects.Interfaces.Repositories;
public interface IDesignHistoryRepository
{
    public Task<OperationResponse> GetAllAsync(string name, string date, string designId);
    public Task<OperationResponse> DeleteAsync(int id);
}