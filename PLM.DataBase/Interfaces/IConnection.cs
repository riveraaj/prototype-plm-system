namespace PLM.DataBase.Interfaces;
internal interface IConnection
{
    public Task<SqlConnection> OpenConnectionAsync();
    public Task<SqlConnection> CloseConnectionAsync();
}