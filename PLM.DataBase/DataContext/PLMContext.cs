namespace PLM.DataBase.DataContext;
internal class PLMContext : IConnection
{
    private readonly SqlConnection connection = new(@"server=localhost; database=projectplmdb; user id=anonymous_login; password=11723;");

    public async Task<SqlConnection> OpenConnectionAsync()
    {
        try
        {
            //Validate connection status is closed
            if (connection.State == ConnectionState.Closed) await connection.OpenAsync();
            return connection;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }

    public async Task<SqlConnection> CloseConnectionAsync()
    {
        try
        {
            //Validate connection status is open
            if (connection.State == ConnectionState.Open) await connection.CloseAsync();
            return connection;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }
}