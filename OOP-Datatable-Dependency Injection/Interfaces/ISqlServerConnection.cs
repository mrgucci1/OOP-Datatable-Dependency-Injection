using System.Data.SqlClient;

namespace OOP_Datatable_Dependency_Injection
{
    public interface ISqlServerConnection
    {
        SqlConnection connect(string databaseName, string username, string password, string serverName);
    }
}