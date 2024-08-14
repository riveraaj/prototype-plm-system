namespace PLM.DataBase.Repositories;
internal class ReviewDesignRepository(IConnection context)
    : InvokeStoredProcedure(context), IReviewDesignRepository
{
    public async Task<OperationResponse> CreateAsync(ReviewDesign oReviewDesign,
                                                     char status)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@status", status},
                {"@user_employee_id", oReviewDesign.UserEmployeeId},
                {"@design_id", oReviewDesign.DesignId},
                {"@justification", oReviewDesign.Justification}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("create_review_design", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> GetAllAsync(string userName, string designName)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@userName", userName},
                {"@designName", designName}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("get_review_design", parameters);
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