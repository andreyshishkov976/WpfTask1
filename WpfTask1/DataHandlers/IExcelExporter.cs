using System.Collections.Generic;

namespace WpfTask1.DataHandlers
{
    interface IExcelExporter<T> where T : class
    {
        void ExcelExport(ICollection<T> items, string filePath);
    }
}
