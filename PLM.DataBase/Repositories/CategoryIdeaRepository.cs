namespace PLM.DataBase.Repositories;
internal class CategoryIdeaRepository(IConnection context)
    : InvokeStoredProcedure(context), ICategoryIdeaRepository
{
    public async Task<OperationResponse> GetAllAsync()
    {
        try
        {
            //Execute the stored procedure to create the passenger
            return await Handle("get_category_idea", []);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }
}