namespace PLM.BusinessObjects.Interfaces.Repositories;
public interface IIdeaRepository
{
    public Task<OperationResponse> GetAllAsync(string name, string date);
    public Task<OperationResponse> GetAllForProductProposalAsync();
    public Task<OperationResponse> CreateAsync(Idea oIdea);
    public Task<OperationResponse> UpdateAsync(Idea oIdea);
    public Task<OperationResponse> DeleteAsync(int id);
}