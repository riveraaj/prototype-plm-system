namespace PLM.BusinessObjects.Interfaces.Repositories;
public interface IProductProposalRepository
{
    public Task<OperationResponse> GetAllAsync(string name, string date);
    public Task<OperationResponse> CreateAsync(ProductProposal oProductProposal);
    public Task<OperationResponse> UpdateAsync(ProductProposal oProductProposal);
    public Task<OperationResponse> DeleteAsync(int id);
}