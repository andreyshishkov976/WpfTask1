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
        private SaveFileDialog _saveFileDialog;
        public ExcelPeopleExporter()
        {
            _saveFileDialog = new SaveFileDialog();
            _saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx;";
            _saveFileDialog.InitialDirectory = Environment.CurrentDirectory + @"\Export\";
            _saveFileDialog.CheckPathExists = true;
        }



        public void ExcelExport(ICollection<People> items)
        {
            if (_saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    ExcelMapper mapper = new ExcelMapper();
                    mapper.SaveAsync(_saveFileDialog.FileName, items, "ExportedData", true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
