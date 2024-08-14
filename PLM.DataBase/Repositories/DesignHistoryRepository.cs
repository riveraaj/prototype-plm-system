namespace PLM.DataBase.Repositories;
internal class DesignHistoryRepository(IConnection context)
    : InvokeStoredProcedure(context), IDesignHistoryRepository
{
    public async Task<OperationResponse> DeleteAsync(int id)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@design_history_id", id}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("delete_design_history", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<OperationResponse> GetAllAsync(string name, string date, string designId)
    {
        try
        {
            //Prepare parameters for the stored procedure
            Dictionary<string, object> parameters = new() {
                {"@name", name},
                {"@upload_date", date},
                {"@design_id", designId}
            };

            //Execute the stored procedure to create the passenger
            return await Handle("get_design_history", parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }
}