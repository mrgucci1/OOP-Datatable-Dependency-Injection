using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace OOP_Datatable_Dependency_Injection
{
    public class ExcelControl : IExcelControl
    {
        public int rowCountF { get; set; }
        public int colCountF { get; set; }
        public object[,] excelMasterStart(string path, int sheet)
        {
            //Create Excel Objects 
            Excel.Application ExcelApp;
            Excel._Workbook ExcelWorkbook;
            Excel._Worksheet ExcelSheet;
            //Initilize ExcelApp
            ExcelApp = new Excel.Application();
            //Open current Excel file
            Console.WriteLine($"Opening: {path}");
            ExcelWorkbook = ExcelApp.Workbooks.Open(path);
            //Set Active Sheet
            ExcelSheet = ExcelWorkbook.Sheets[sheet];
            //ExcelApp.Visible = true;
            Excel.Range ExcelRange = ExcelSheet.UsedRange;
            int rowCount = ExcelRange.Rows.Count;
            int colCount = ExcelRange.Columns.Count;
            rowCountF = rowCount;
            colCountF = colCount;
            object[,] excelValues = (object[,])ExcelRange.Value2;
            //Close and release all COM objects, quit excel
            Marshal.ReleaseComObject(ExcelRange);
            Marshal.ReleaseComObject(ExcelSheet);
            //close and release
            ExcelWorkbook.Close();
            Marshal.ReleaseComObject(ExcelWorkbook);
            //quit and release
            ExcelApp.Quit();
            Marshal.ReleaseComObject(ExcelApp);
            return excelValues;
        }
        public object[,] filterExcel(object[,] excelValues, int startingRow)
        {
            for (int rows = startingRow; rows < rowCountF + 1; rows++)
            {
                //Cleanup excel data, remove null cells and replace single quotes
                for (int cols = 1; cols < colCountF + 1; cols++)
                {
                    if (excelValues[rows, cols] == null || excelValues[rows, cols].ToString() == "")
                        excelValues[rows, cols] = DBNull.Value;
                    if (excelValues[rows, cols] != DBNull.Value && excelValues[rows, cols].ToString().Contains("'")) ;
                    excelValues[rows, cols] = excelValues[rows, cols].ToString().Replace("'", "''").Trim(); //Replace quotes with DB friendly ones
                }
            }
            return excelValues;
        }
    }
}
