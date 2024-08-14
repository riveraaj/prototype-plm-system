namespace PLM.DataBase.Repositories;
internal class DesignCommentRepository(IConnection context)
    : InvokeStoredProcedure(context), IDesignCommentRepository
{
    public async Task<OperationResponse> CreateAsync(DesignComment oDesignComment)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@description", oDesignComment.Description},
                {"@user_employee_id", oDesignComment.UserEmployeeId},
                {"@design_id", oDesignComment.DesignId}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("create_design_comment", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> GetByDesignIdAsync(int id)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@design_id", id}

            };

            //Execute the stored procedure to create the passenger
            return await Handle("get_by_design_id_comment", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }
}