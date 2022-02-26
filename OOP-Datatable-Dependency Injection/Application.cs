using System.Data;
using System.Data.SqlClient;

namespace OOP_Datatable_Dependency_Injection
{
    public class Application : IApplication
    {
        IDataTableControl _dataTableControl;
        ISqlServerConnection _connection;
        public Application(IDataTableControl dataTableControl, ISqlServerConnection sqlServerConnection)
        {
            _dataTableControl = dataTableControl;
            _connection = sqlServerConnection;
        }
        public void Run()
        {
            //Configuration
            //Place the SQL Information Here 
            string serverName = "";
            string dataBaseName = "";
            string tableName = "";
            string username = "";
            string password = "";
            //Place excel config here
            int startingRow = 2;
            string fileType = ".csv";
            //Create data table
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("insertdate", typeof(DateTime)));
            tbl.Columns.Add(new DataColumn("id", typeof(int)));
            tbl.Columns.Add(new DataColumn("name", typeof(string)));
            tbl.Columns.Add(new DataColumn("salary", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("department", typeof(string)));
            //Populate Table
            tbl = _dataTableControl.tablePopulate(tbl, startingRow, fileType);
            //Get SQL Connection
            SqlConnection cnn = _connection.connect(dataBaseName, username, password, serverName);
            //Insert Table
            _dataTableControl.tableInsert(tbl, cnn, dataBaseName, tableName);
            Console.WriteLine("Complete, press any key to exit");
            Console.ReadKey();
        }
    }
}
