namespace PLM.BusinessObjects.Interfaces.Repositories;
public interface IReviewDesignRepository
{
    public Task<OperationResponse> GetAllAsync(string userName, string designName);
    public Task<OperationResponse> CreateAsync(ReviewDesign oReviewDesign, char status);
}