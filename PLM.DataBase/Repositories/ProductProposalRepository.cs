namespace PLM.DataBase.Repositories;
internal class ProductProposalRepository(IConnection context)
    : InvokeStoredProcedure(context), IProductProposalRepository
{
    public async Task<OperationResponse> CreateAsync(ProductProposal oProductProposal)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@idea_id", oProductProposal.IdeaId},
                {"@user_employee_id", oProductProposal.UserEmployeeId},
                {"@file_path", oProductProposal.FilePath}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("create_product_proposal", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> DeleteAsync(int id)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@product_proposal_id", id}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("delete_product_proposal", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> GetAllAsync(string name, string date)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@ideaName", name},
                {"@date_creation", date}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("get_product_proposal", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> UpdateAsync(ProductProposal oProductProposal)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@product_proposal_id", oProductProposal.ProductProposalId},
                {"@file_path", oProductProposal.FilePath},
                {"@user_employee_id", oProductProposal.UserEmployeeId}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("update_product_proposal", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }
}