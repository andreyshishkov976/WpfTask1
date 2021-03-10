using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTask1.Models;

namespace WpfTask1.DataHandlers
{
    interface IExcelExporter<T> where T : class
    {
        void ExcelExport(ICollection<T> items, string filePath);
    }
}
