namespace PLM.BusinessObjects.Interfaces.Repositories;
public interface IDesignRepository
{
    public Task<OperationResponse> GetAllAsync(string name, string lastModification);
    public Task<OperationResponse> CreateAsync(Design oDesign);
    public Task<OperationResponse> UpdateAsync(Design oDesign);
    public Task<OperationResponse> DeleteAsync(int id);
}