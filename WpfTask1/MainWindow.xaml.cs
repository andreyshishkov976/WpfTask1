using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task3;
using Task3.DataHandlers;
using Task3.Interfaces;
using Task3.Models;

namespace WpfTask1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog fileDialog;
        IDataBaseHandler dataBaseHandler;
        ICsvFileHandler csvFileHandler;
        public MainWindow()
        {
            InitializeComponent();
            fileDialog = new OpenFileDialog();
            fileDialog.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt";
            fileDialog.InitialDirectory = Environment.CurrentDirectory;
            fileDialog.Multiselect = false;
            csvFileHandler = new CSVfileHandler();
            dataBaseHandler = new DataBaseHandler();
            //dataBaseHandler.SelectPeopleData(ref PeopleGrid);
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //PeopleGrid.ItemsSource = await dataBaseHandler.LoadPeopleTable();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dataBaseHandler.DisposeConnection();
        }

        private async void LoadCSVButton_Click(object sender, RoutedEventArgs e)
        {
            if (fileDialog.ShowDialog() == true)
            {
                await dataBaseHandler.ImportPeopleData(await csvFileHandler.DataLoaderAsync(fileDialog.FileName));
            }
        }

        private async void LoadDBButton_Click(object sender, RoutedEventArgs e)
        {
            PeopleGrid.ItemsSource = await dataBaseHandler.LoadPeopleTable();
        }
    }
}
