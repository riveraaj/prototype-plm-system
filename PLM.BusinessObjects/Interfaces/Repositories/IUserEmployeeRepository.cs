namespace PLM.BusinessObjects.Interfaces.Repositories;
public interface IUserEmployeeRepository
{
    public Task<OperationResponse> GetAllAsync(string name, string id);
    public Task<OperationResponse> GetByIdAsync(int id);
    public Task<OperationResponse> CreateAsync(UserEmployee oUserEmployee);
    public Task<OperationResponse> UpdateAsync(UserEmployee oUserEmployee);
    public Task<OperationResponse> DeleteAsync(int id);
}