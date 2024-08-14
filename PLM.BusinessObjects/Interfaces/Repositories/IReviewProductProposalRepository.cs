namespace PLM.BusinessObjects.Interfaces.Repositories;
public interface IReviewProductProposalRepository
{
    public Task<OperationResponse> GetAllAsync(string userName, string ideaName);
    public Task<OperationResponse> GetAllForDesign();
    public Task<OperationResponse> CreateAsync(ReviewProductProposal oReviewProductProposal, char status);
}