namespace PLM.DataBase.Repositories;
internal class IdeaRepository(IConnection context)
    : InvokeStoredProcedure(context), IIdeaRepository
{
    public async Task<OperationResponse> CreateAsync(Idea oIdea)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@name", oIdea.Name}, {"@description", oIdea.Description},
                {"@user_employee_id", oIdea.UserEmployeeId}, {"@category_idea_id", oIdea.CategoryIdeaId}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("create_idea", parameters);
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
                {"@idea_id", id}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("delete_idea", parameters);
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
            return await Handle("get_idea", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> GetAllForProductProposalAsync()
    {
        try
        {
            //Execute the stored procedure to create the passenger
            return await Handle("get_idea_for_product_proposal", []);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> UpdateAsync(Idea oIdea)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@idea_id", oIdea.IdeaId},
                {"@status", oIdea.Status},
                {"@user_employee_id", oIdea.UserEmployeeId}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("update_idea", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }
}