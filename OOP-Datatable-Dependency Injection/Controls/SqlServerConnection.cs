
using System.Data.SqlClient;
namespace OOP_Datatable_Dependency_Injection
{
    public class SqlServerConnection : ISqlServerConnection
    {
        public SqlConnection connect(string databaseName, string username, string password, string serverName)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={serverName};Initial Catalog={databaseName};User ID={username};Password={password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            return cnn;
        }
    }
}
