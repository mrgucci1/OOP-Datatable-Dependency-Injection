namespace OOP_Datatable_Dependency_Injection
{
    public interface IExcelControl
    {
        int colCountF { get; set; }
        int rowCountF { get; set; }

        object[,] excelMasterStart(string path, int sheet);
        object[,] filterExcel(object[,] excelValues, int startingRow);
    }
}