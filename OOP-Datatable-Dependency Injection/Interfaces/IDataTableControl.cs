using System.Data;
using System.Data.SqlClient;

namespace OOP_Datatable_Dependency_Injection
{
    public interface IDataTableControl
    {
        void tableInsert(DataTable tbl, SqlConnection cnn, string database, string tableName);
        DataTable tablePopulate(DataTable tbl, int startingRow, string fileType);
    }
}