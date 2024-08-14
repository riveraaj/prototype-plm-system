namespace PLM.DataBase.Repositories;
internal class ReviewProductProposalRepository(IConnection context)
    : InvokeStoredProcedure(context), IReviewProductProposalRepository
{
    public async Task<OperationResponse> CreateAsync(ReviewProductProposal oReviewProductProposal,
                                                     char status)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@status", status},
                {"@user_employee_id", oReviewProductProposal.UserEmployeeId},
                {"@product_proposal_id", oReviewProductProposal.ProductProposalId},
                {"@justification", oReviewProductProposal.Justification}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("create_review_product_proposal", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> GetAllAsync(string userName, string ideaName)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@userName", userName},
                {"@ideaName", ideaName}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("get_review_product_proposal", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> GetAllForDesign()
    {
        try
        {
            //Execute the stored procedure to create the passenger
            return await Handle("get_review_product_proposal_for_design", []);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }
}