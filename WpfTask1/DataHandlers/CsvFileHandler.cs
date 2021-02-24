using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task3.Interfaces;

namespace Task3
{
    class CSVfileHandler : ICsvFileHandler
    {
        private OpenFileDialog _openFileDialog;
        public CSVfileHandler()
        {
            _openFileDialog = new OpenFileDialog();
            _openFileDialog.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt";
            _openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            _openFileDialog.Multiselect = false;
        }

        public List<Person> DataLoader()
        {
            List<Person> people = new List<Person>();
            if (_openFileDialog.ShowDialog() == true)
                people = File.ReadAllLines(_openFileDialog.FileName)
                                               .Select(v => new Person(v))
                                               .ToList();
            else
            {
                MessageBox.Show("You did not choose the file.\nThe application will continue to work.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return people;
        }
    }
}
