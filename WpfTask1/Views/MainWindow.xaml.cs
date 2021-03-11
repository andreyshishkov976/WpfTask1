using System.Windows;
using System.Windows.Controls;
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

        }

        private void PeopleGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TabControl.SelectedItem = TabControl.Items[1];
        }

        private void AddPeople_Click(object sender, RoutedEventArgs e)
        {
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
                    target.ClearValue(DatePicker.TextProperty);
                    target.ClearValue(TextBox.TextProperty);
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

        private void DateTimePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Control control = sender as Control;
            control.ClearValue(Border.BorderBrushProperty);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Control control = sender as Control;
            control.ClearValue(Border.BorderBrushProperty);
        }

        private void IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
