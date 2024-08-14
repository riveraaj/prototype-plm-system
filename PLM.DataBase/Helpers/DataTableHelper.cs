namespace PLM.DataBase.Helpers;
/// <summary>
/// Provides helper methods for handling and converting DataTables.
/// </summary>
internal static class DataTableHelper
{
    /// <summary>
    /// Converts a DataTable to an OperationResponse with the specified code and message.
    /// </summary>
    /// <param name="code">The response code to include in the OperationResponse.</param>
    /// <param name="message">The message to include in the OperationResponse.</param>
    /// <param name="oDataTable">The DataTable to convert. If null, the method returns a response with the initial code and message.</param>
    /// <returns>An <see cref="OperationResponse"/> containing the converted DataTable or the initial code and message.</returns>
    public static OperationResponse ConvertDataTable(short code, string message,
                                                     DataTable oDataTable = null)
    {
        try
        {
            //If the DataTable is null, return a ResponseDB with the initial code and message
            if (oDataTable == null) return new OperationResponse([], code, message);

            //Read messages returned by the stored procedure from the result set
            if (oDataTable.Rows.Count > 0)
            {
                if (oDataTable.Columns[0].ColumnName.Equals("Message_ID"))
                {
                    var codeString = oDataTable.Rows[0][0].ToString();
                    var msg = oDataTable.Rows[0][1].ToString();

                    if (!string.IsNullOrEmpty(codeString)) code = short.Parse(codeString);
                    if (!string.IsNullOrEmpty(msg)) message = msg;

                    oDataTable.Rows.RemoveAt(0);
                }
            }

            //Convert the DataTable to a List<object>
            var content = DataTableHelper.DataTableToListObject(oDataTable);

            return new OperationResponse(content, code, message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Converts a DataTable to a List of dynamic objects.
    /// </summary>
    /// <param name="oDataTable">The DataTable to convert.</param>
    /// <returns>A List of dynamic objects representing the rows of the DataTable.</returns>
    private static List<object> DataTableToListObject(DataTable oDataTable)
    {
        try
        {
            //Create a list to hold the result
            List<object> resultList = [];

            //Iterate through each DataRow in the DataTable
            foreach (DataRow row in oDataTable.Rows)
            {
                var rowData = new ExpandoObject() as IDictionary<string, Object>;

                //Iterate through each column in the DataRow
                foreach (DataColumn col in oDataTable.Columns)
                    rowData.Add(col.ColumnName, row[col]);

                //Add the ExpandoObject to the result list
                resultList.Add(rowData);
            }
            return resultList;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}