namespace PLM.DataBase.Repositories;
internal class DesignRepository(IConnection context)
    : InvokeStoredProcedure(context), IDesignRepository
{
    public async Task<OperationResponse> CreateAsync(Design oDesign)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@name", oDesign.Name},
                {"@current_design_path", oDesign.CurrentDesignPath},
                {"@review_product_proposal_id", oDesign.ReviewProductProposalId},
                {"@user_employee_id", oDesign.UserEmployeeId}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("create_design", parameters);
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
                {"@design_id", id}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("delete_design", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> GetAllAsync(string name, string lastModification)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@name", name},
                {"@last_modification", lastModification}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("get_design", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> UpdateAsync(Design oDesign)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@design_id", oDesign.DesignId},
                {"@current_design_path", oDesign.CurrentDesignPath},
                {"@user_employee_id", oDesign.UserEmployeeId}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("update_design", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }
}