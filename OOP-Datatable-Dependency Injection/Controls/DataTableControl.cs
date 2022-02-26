using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace OOP_Datatable_Dependency_Injection
{
    public class DataTableControl : IDataTableControl
    {
        IExcelControl _excelControl;
        public DataTableControl(IExcelControl excelControl)
        {
            _excelControl = excelControl;
        }
        //Max length for error message
        const int maxLength = 200;
        //Populate Datatable with data from .csv/.xlsx file
        public DataTable tablePopulate(DataTable tbl, int startingRow, string fileType)
        {
            //Find PO Excel Doc, insert into DataTable
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), $"*{fileType}");
            if (files.Length < 1)
            {
                Console.WriteLine("No files Found, Press any key to exit...");
                Console.ReadKey();
                System.Environment.Exit(1);
            }
            int sheet = 1;
            object[,] excelValues = _excelControl.excelMasterStart(files[0], sheet);
            int success = 0;
            excelValues = _excelControl.filterExcel(excelValues, startingRow);
            //Timer for Conversion Process
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Console.WriteLine($"Processing Data file with {excelValues.GetLength(0)} rows...");
            for (int i = startingRow; i < excelValues.GetLength(0) - 1; i++)
            {
                DataRow dr = tbl.NewRow();
                //Populate DataRow with excelValue objects
                for (int index = 1; index < tbl.Columns.Count + 1; index++)
                {
                    try { dr[tbl.Columns[index - 1].ColumnName] = excelValues[i, index]; }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Failed when adding data to DataTable\nFailed on row: {index}\nAttempted to add {excelValues[i, index]} to column named {tbl.Columns[index - 1].ColumnName}");
                        Console.WriteLine($"This column is of type: {tbl.Columns[index - 1].DataType}\nError Message: \n{e.ToString().Substring(0, maxLength)}");
                        Console.ResetColor();
                        Console.WriteLine("Press any Key to exit...");
                        Console.ReadKey();
                        System.Environment.Exit(1);
                    }
                }
                tbl.Rows.Add(dr);
                success++;
                //Every 10,000 rows, report to user information about process
                if (success % 10000 == 0 && success != 0)
                {
                    decimal percent = Convert.ToDecimal(success) / Convert.ToDecimal(excelValues.GetLength(0));
                    decimal final = percent * 100;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Proccessing Data...\nJUST PASSED ROW: {success}\nElapsed Time: {timer.Elapsed}\nPercent Complete = {final.ToString("0.##")}%\n");
                    Console.ResetColor();
                }
            }
            timer.Stop();
            return tbl;
        }
        public void tableInsert(DataTable tbl, SqlConnection cnn, string database, string tableName)
        {
            SqlBulkCopy objBulk = new SqlBulkCopy(cnn);
            objBulk.DestinationTableName = tableName;
            //Map DataTable Headers to SQL Database Headers
            for (int i = 0; i < tbl.Columns.Count; i++)
                objBulk.ColumnMappings.Add(tbl.Columns[i].ColumnName, tbl.Columns[i].ColumnName);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Console.WriteLine("Inserting....");
            objBulk.WriteToServer(tbl);
            cnn.Close();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Completed insertion into {database} table {tableName}\nElapsed time: {timer.Elapsed}");
            Console.ResetColor();
            timer.Stop();
        }

    }
}
