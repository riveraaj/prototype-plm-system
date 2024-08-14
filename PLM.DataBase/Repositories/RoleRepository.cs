namespace PLM.DataBase.Repositories;
internal class RoleRepository(IConnection context)
    : InvokeStoredProcedure(context), IRoleRepository
{
    public async Task<OperationResponse> GetAllAsync()
    {
        try
        {
            //Execute the stored procedure to create the passenger
            return await Handle("get_role", []);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }
}