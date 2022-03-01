using OOP_Datatable_Dependency_Injection;
using Xunit;

namespace OOP_DataTable_Dependency_Injection.Tests
{
    public class DataTableControlTests
    {
        [Fact]
        public void getFile_ShouldWould()
        {
            //arrange
            string[] files = new string[] { "file.csv" };
            DataTableControl dtctrl = new DataTableControl(null);
            //act
            string actual = dtctrl.getFile(files);
            //assert
            Assert.Equal(files[0], actual);
        }
        [Fact]
        public void getFile_MultipleFilesShouldFail()
        {
            //arrange
            string[] files = new string[] { "file.csv", "file.csv" };
            DataTableControl dtctrl = new DataTableControl(null);
            //act
            Exception e = Assert.Throws<Exception>(() => dtctrl.getFile(files));
            //assert
            Assert.Equal("Multiple files found matching filetype, please remove extra files", e.Message);
        }
        [Fact]
        public void getFile_NoFilesShouldFail()
        {
            //arrange
            string[] files = null;
            DataTableControl dtctrl = new DataTableControl(null);
            //act
            Exception e = Assert.Throws<Exception>(() => dtctrl.getFile(files));
            //assert
            Assert.Equal("No files Found", e.Message);
        }
    }
}
