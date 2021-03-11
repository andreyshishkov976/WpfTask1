using Ganss.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using WpfTask1.Models;

namespace WpfTask1.DataHandlers
{
    class ExcelPeopleExporter : IExcelExporter<People>
    {
        public void ExcelExport(ICollection<People> items, string filePath)
        {
            ExcelMapper mapper = new ExcelMapper();
            mapper.SaveAsync(filePath, items, "ExportedData", true);
        }
    }
}
