using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfTask1.ViewModels;

namespace WpfTask1.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void AddPeople1_Click(object sender, RoutedEventArgs e)
        {
            //PeopleGrid.SelectedItem = null;
        }

        private void PeopleGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TabControl.SelectedItem = TabControl.Items[1];
        }

        private void AddPeople_Click(object sender, RoutedEventArgs e)
        {
            //AddDateOfBirth.SelectedDate = DateTime.Now;
            //AddDateOfBirth.Text = string.Empty;
            //AddName.Text = string.Empty;
            //AddLastName.Text = string.Empty;
            //AddSurName.Text = string.Empty;
            //AddCity.Text = string.Empty;
            //AddCountry.Text = string.Empty;
        }

        private void IsEnabledSwitch(CheckBox sender, Control target)
        {
            switch (sender.IsChecked)
            {
                case true:
                    target.IsEnabled = true;
                    break;
                default:
                    target.IsEnabled = false;
                    break;
            }
        }

        private void FilterByDateSwitch(object sender, RoutedEventArgs e)
        {
            IsEnabledSwitch((CheckBox)sender, DateFilter);
        }

        private void FilterByNameCheck_Click(object sender, RoutedEventArgs e)
        {
            IsEnabledSwitch((CheckBox)sender, NameFilter);
        }

        private void FilterBySurNameCheck_Click(object sender, RoutedEventArgs e)
        {
            IsEnabledSwitch((CheckBox)sender, SurNameFilter);
        }

        private void FilterByCityCheck_Click(object sender, RoutedEventArgs e)
        {
            IsEnabledSwitch((CheckBox)sender, CityFilter);
        }

        private void FilterByCountryCheck_Click(object sender, RoutedEventArgs e)
        {
            IsEnabledSwitch((CheckBox)sender, CountryFilter);
        }

        private void FilterByLastNameCheck_Click(object sender, RoutedEventArgs e)
        {
            IsEnabledSwitch((CheckBox)sender, LastNameFilter);
        }
    }
}
