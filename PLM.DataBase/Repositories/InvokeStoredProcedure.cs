namespace PLM.DataBase.Repositories;
/// <summary>
/// Handles the invocation of stored procedures in a database.
/// <param name="context">The database connection context.</param>
/// </summary>
internal class InvokeStoredProcedure(IConnection context)
{
    private readonly IConnection _connection = context;

    /// <summary>
    /// Executes a stored procedure with the given parameters and returns the results.
    /// </summary>
    /// <param name="storedProcedureName">The name of the stored procedure to execute.</param>
    /// <param name="parameters">A dictionary of parameters to pass to the stored procedure.</param>
    /// <returns>An <see cref="OperationResponse"/> containing the results of the stored procedure.</returns>
    protected async Task<OperationResponse> Handle(string storedProcedureName,
                                          Dictionary<string, object> parameters)
    {
        //Initialize the DataTable
        DataTable dataTable = new();

        try
        {
            //Open a connection to the DB
            await using var connection = await _connection.OpenConnectionAsync();

            //Create a command to execute the stored procedure
            using var command = new SqlCommand(storedProcedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            //Add parameters to the command
            foreach (var parameter in parameters)
            {
                if (parameter.Value is DateOnly dateOnlyValue) command.Parameters.AddWithValue(parameter.Key, dateOnlyValue.ToDateTime(TimeOnly.MinValue));
                else command.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }

            //Execute the SP and read the results into a SqlDataReader
            await using var dataReader = await command.ExecuteReaderAsync();

            //Load the results from the dataReader into the DataTable
            dataTable.Load(dataReader);

            //Convert and return DataTable to ResponseDB
            return DataTableHelper.ConvertDataTable(2, "No se ha recibido ningún mensaje", dataTable);
        }
        catch (Exception ex)
        {
            //Handle exceptions
            short code = -3;
            string message = "Se ha producido un problema, inténtelo de nuevo. Si el problema persiste, póngase en contacto con el servicio de asistencia técnica";

            Console.WriteLine(ex.Message);

            //Convert and return DataTable to ResponseDB
            return DataTableHelper.ConvertDataTable(code, message);
        }
    }
}