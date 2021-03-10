using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfTask1.DataHandlers;
using WpfTask1.Models;
using WpfTask1.Repositories;
using WpfTask1.Specifications;

namespace WpfTask1.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            PeopleCollection = new ObservableCollection<People>();
            _peopleRepository = new PeopleRepository();
            _csvFileHandler = new CSVPeopleImporter();
            _excelExporter = new ExcelPeopleExporter();
            _jsonSerializer = new JsonPeopleSerializer();
            _saveExcelDialog = new SaveFileDialog();
            _saveExcelDialog.Filter = "Excel Files|*.xls;*.xlsx;";
            _saveExcelDialog.InitialDirectory = Environment.CurrentDirectory + @"\Export\";
            _saveExcelDialog.CheckPathExists = true;
            _saveJsonDialog = new SaveFileDialog();
            _saveJsonDialog.Filter = "JSON Files|*.json;";
            _saveJsonDialog.InitialDirectory = Environment.CurrentDirectory + @"\Export\";
            _saveJsonDialog.CheckPathExists = true;
            LoadDB(this);
            LoadCommand = new DelegateCommand(LoadDB);
            AddCommand = new DelegateCommand(AddPeople);
            RemoveCommand = new DelegateCommand(RemovePeople, CanRemovePeople);
            UpdateCommand = new DelegateCommand(UpdatePeople);
            ImportCommand = new DelegateCommand(ImportCSV);
            FilterCommand = new DelegateCommand(FilterData);
            ExcelExport = new DelegateCommand(ExportToExcel);
            JsonExport = new DelegateCommand(ExportToJson);
            BrowseExcelCommand = new DelegateCommand(ShowSaveExcelDialog);
            BrowseJsonCommand = new DelegateCommand(ShowSaveJsonDialog);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private People _selectedPeople;
        private ICollection<People> _peopleCollection;
        public People SelectedPeople
        {
            get { return _selectedPeople; }
            set
            {
                _selectedPeople = value;
                OnPropertyChanged("SelectedPeople");
            }
        }
        public ICollection<People> PeopleCollection
        {
            get { return _peopleCollection; }
            set
            {
                _peopleCollection = value;
                OnPropertyChanged("PeopleCollection");
            }
        }

        #region ADDITION FIELDS
        private string _dateOfBirth;
        private string _name;
        private string _lastName;
        private string _surName;
        private string _city;
        private string _country;
        public string AddDateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }
        public string AddName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string AddLastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public string AddSurName
        {
            get { return _surName; }
            set
            {
                _surName = value;
                OnPropertyChanged("SurName");
            }
        }
        public string AddCity
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }
        public string AddCountry
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged("Country");
            }
        }
        #endregion
        
        #region FILTER FIELDS
        private string _dateFilter;
        private string _nameFilter;
        private string _lastNameFilter;
        private string _surNameFilter;
        private string _cityFilter;
        private string _countryFilter;
        public string DateFilter
        {
            get { return _dateFilter; }
            set
            {
                _dateFilter = value;
                OnPropertyChanged("DateFilter");
            }
        }
        public string NameFilter
        {
            get { return _nameFilter; }
            set
            {
                _nameFilter = value;
                OnPropertyChanged("NameFilter");
            }
        }
        public string LastNameFilter
        {
            get { return _lastNameFilter; }
            set
            {
                _lastNameFilter = value;
                OnPropertyChanged("LastNameFilter");
            }
        }
        public string SurNameFilter
        {
            get { return _surNameFilter; }
            set
            {
                _surNameFilter = value;
                OnPropertyChanged("SurNameFilter");
            }
        }
        public string CityFilter
        {
            get { return _cityFilter; }
            set
            {
                _cityFilter = value;
                OnPropertyChanged("CityFilter");
            }
        }
        public string CountryFilter
        {
            get { return _countryFilter; }
            set
            {
                _countryFilter = value;
                OnPropertyChanged("CountryFilter");
            }
        }
        #endregion
        
        #region INTERFACES
        private IRepository<People> _peopleRepository;
        private ICsvImporter<People> _csvFileHandler;
        private IExcelExporter<People> _excelExporter;
        private IJsonSerializer<People> _jsonSerializer;
        #endregion

        #region COMMANDS
        public ICommand LoadCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand FilterCommand { get; private set; }
        public ICommand ExcelExport { get; private set; }
        public ICommand JsonExport { get; private set; }
        public ICommand BrowseExcelCommand { get; private set; }
        public ICommand BrowseJsonCommand { get; private set; }
        #endregion

        #region DATABASE OPERATIONS
        private void LoadDB(object obj)
        {
            _peopleRepository.LoadDB();
            PeopleCollection = _peopleRepository.GetObjectsList();
        }
        private void AddPeople(object obj)
        {
            if (AddDateOfBirth == null || AddDateOfBirth.Replace(" ", "").Length == 0)
                EmptyFieldMessage(obj as Window, "AddDateOfBirth");
            else if (AddName == null || AddName.Replace(" ", "").Length == 0)
                EmptyFieldMessage(obj as Window, "AddName");
            else if (AddLastName == null || AddLastName.Replace(" ", "").Length == 0)
                EmptyFieldMessage(obj as Window, "AddLastName");
            else if (AddSurName == null || AddSurName.Replace(" ", "").Length == 0)
                EmptyFieldMessage(obj as Window, "AddSurName");
            else if (AddCity == null || AddCity.Replace(" ", "").Length == 0)
                EmptyFieldMessage(obj as Window, "AddCity");
            else if (AddCountry == null || AddCountry.Replace(" ", "").Length == 0)
                EmptyFieldMessage(obj as Window, "AddCoutry");
            else
            {
                _peopleRepository.CreateObject(new People(DateTime.Parse(AddDateOfBirth).Date, AddName, AddLastName, AddSurName, AddCity, AddCountry));
                _peopleRepository.Save();
                ClearAddedPeople();
            }
        }
        private void RemovePeople(object obj)
        {
            _peopleRepository.DeleteObject((People)obj);
            _peopleRepository.Save();
        }
        private void UpdatePeople(object obj)
        {
            if (SelectedPeople.DateOfBirth == null)
                EmptyFieldMessage(obj as Window, "EditDateOfBirth");
            else if (SelectedPeople.Name == null || SelectedPeople.Name.Replace(" ", "").Length == 0)
                EmptyFieldMessage(obj as Window, "EditName");
            else if (SelectedPeople.LastName == null || SelectedPeople.LastName.Replace(" ", "").Length == 0)
                EmptyFieldMessage(obj as Window, "EditLastName");
            else if (SelectedPeople.SurName == null || SelectedPeople.SurName.Replace(" ", "").Length == 0)
                EmptyFieldMessage(obj as Window, "EditSurName");
            else if (SelectedPeople.City == null || SelectedPeople.City.Replace(" ", "").Length == 0)
                EmptyFieldMessage(obj as Window, "EditCity");
            else if (SelectedPeople.Country == null || SelectedPeople.Country.Replace(" ", "").Length == 0)
                EmptyFieldMessage(obj as Window, "EditCoutry");
            else
            {
                _peopleRepository.UpdateObject(SelectedPeople);
                SelectedPeople = null;
                _peopleRepository.Save();
            }
        }
        private void FilterData(object obj)
        {
            Window mainWindow = (Window)obj;
            List<Specification<People>> SpecList = new List<Specification<People>>();
            CheckBox DateCheck = mainWindow.FindName("FilterByDateCheck") as CheckBox;
            CheckBox NameCheck = mainWindow.FindName("FilterByNameCheck") as CheckBox;
            CheckBox LNameCheck = mainWindow.FindName("FilterByLastNameCheck") as CheckBox;
            CheckBox SNameCheck = mainWindow.FindName("FilterBySurNameCheck") as CheckBox;
            CheckBox CityCheck = mainWindow.FindName("FilterByCityCheck") as CheckBox;
            CheckBox CountryCheck = mainWindow.FindName("FilterByCountryCheck") as CheckBox;
            if (DateCheck.IsChecked == true)
                if (DateFilter == null || DateFilter.Replace(" ", "").Length == 0)
                    EmptyFieldMessage(mainWindow, "DateFilter");
                else
                    SpecList.Add(new FindByDateSpecification(DateTime.Parse(DateFilter)));

            if (NameCheck.IsChecked == true)
                if (NameFilter == null || NameFilter.Replace(" ", "").Length == 0)
                    EmptyFieldMessage(mainWindow, "NameFilter");
                else
                    SpecList.Add(new FindByNameSpecification(NameFilter));
            if (LNameCheck.IsChecked == true)
                if (LastNameFilter == null || LastNameFilter.Replace(" ", "").Length == 0)
                    EmptyFieldMessage(mainWindow, "LastNameFilter");
                else
                    SpecList.Add(new FindByLastNameSpecification(LastNameFilter));
            if (SNameCheck.IsChecked == true)
                if (SurNameFilter == null || SurNameFilter.Replace(" ", "").Length == 0)
                    EmptyFieldMessage(mainWindow, "SurNameFilter");
                else
                    SpecList.Add(new FindBySurNameSpecification(SurNameFilter));
            if (CityCheck.IsChecked == true)
                if (CityFilter == null || CityFilter.Replace(" ", "").Length == 0)
                    EmptyFieldMessage(mainWindow, "CityFilter");
                else
                    SpecList.Add(new FindByCitySpecification(CityFilter));
            if (CountryCheck.IsChecked == true)
                if (CountryFilter == null || CountryFilter.Replace(" ", "").Length == 0)
                    EmptyFieldMessage(mainWindow, "CountryFilter");
                else
                    SpecList.Add(new FindByCountrySpecification(CountryFilter));
            if (SpecList.Count > 0)
            {
                Specification<People> specification = SpecList[0];
                SpecList.RemoveAt(0);
                foreach (var spec in SpecList)
                {
                    specification.And(spec);
                }
                PeopleCollection = _peopleRepository.Find(specification);
            }
            else
                LoadDB(obj);
        }
        private void ClearAddedPeople()
        {
            AddDateOfBirth = string.Empty;
            AddName = string.Empty;
            AddLastName = string.Empty;
            AddSurName = string.Empty;
            AddCity = string.Empty;
            AddCountry = string.Empty;
        }
        private bool CanRemovePeople(object arg)
        {
            return (arg as People) != null;
        }
        private void EmptyFieldMessage(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
            MessageBox.Show("Одно из полей не было заполнено. Результат не будет сохранен.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region EXPORT PARAMETERS
        private SaveFileDialog _saveExcelDialog;
        private SaveFileDialog _saveJsonDialog;
        private string _jsonFileName;
        public string JsonFileName
        {
            get { return _jsonFileName; }
            set
            {
                _jsonFileName = value;
                OnPropertyChanged("JsonFileName");
            }
        }
        private string _excelFileName;
        public string ExcelFileName
        {
            get { return _excelFileName; }
            set
            {
                _excelFileName = value;
                OnPropertyChanged("ExcelFileName");
            }
        }
        #endregion

        #region IMPORT/EXPORT
        private async void ImportCSV(object obj)
        {
            var range = await _csvFileHandler.DataLoaderAsync(Environment.CurrentDirectory + @"\Import\persons.csv");
            foreach (var item in range)
            {
                _peopleRepository.CreateObject(item);
            }
            _peopleRepository.Save();
        }
        private void ShowSaveExcelDialog(object obj)
        {
            if (_saveExcelDialog.ShowDialog() == true)
                ExcelFileName = _saveExcelDialog.FileName;
        }
        private void ShowSaveJsonDialog(object obj)
        {
            if (_saveJsonDialog.ShowDialog() == true)
                JsonFileName = _saveJsonDialog.FileName;
        }
        private async void ExportToJson(object obj)
        {
            if (JsonFileName == null || JsonFileName.Replace(" ", "").Length == 0)
                EmptyFieldMessage(obj as Window, "JsonFileName");
            else
            {
                await Task.Run(() =>
                {
                    string jsonData = _jsonSerializer.Serialize(PeopleCollection);
                    File.WriteAllText(JsonFileName, jsonData);
                    MessageBox.Show("Экспорт завершен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                });
            }
        }
        private async void ExportToExcel(object obj)
        {
            if (ExcelFileName == null || ExcelFileName.Replace(" ", "").Length == 0)
                EmptyFieldMessage(obj as Window, "ExcelFileName");
            else
            {
                await Task.Run(() =>
                {
                    _excelExporter.ExcelExport(PeopleCollection, ExcelFileName);
                    MessageBox.Show("Экспорт завершен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                });
            }

        }
        #endregion
    }
}
