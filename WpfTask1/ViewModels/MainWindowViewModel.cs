using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        //Selected People
        private People _selectedPeople;
        public People SelectedPeople
        {
            get { return _selectedPeople; }
            set
            {
                _selectedPeople = value;
                OnPropertyChanged("SelectedPeople");
            }
        }
        //Selected People

        //Added People
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
        //Added People

        //Filter People
        private string _dateOfBirthFilter;
        private string _nameFilter;
        private string _lastNameFilter;
        private string _surNameFilter;
        private string _cityFilter;
        private string _countryFilter;
        public string DateFilter
        {
            get { return _dateOfBirthFilter; }
            set
            {
                _dateOfBirthFilter = value;
                OnPropertyChanged("DateOfBirthFilter");
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
        //Filter People

        private IRepository<People> _peopleRepository;
        private ICsvImporter<People> _csvFileHandler;
        private ICollection<People> _peopleCollection;
        public ICollection<People> PeopleCollection
        {
            get { return _peopleCollection; }
            set
            {
                _peopleCollection = value;
                OnPropertyChanged("PeopleCollection");
            }
        }

        //Commands
        public ICommand LoadCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand FilterCommand { get; private set; }
        //Commands

        public MainWindowViewModel()
        {
            PeopleCollection = new ObservableCollection<People>();
            _peopleRepository = new PeopleRepository();
            _csvFileHandler = new CSVPeopleImporter();
            LoadDB(this);
            LoadCommand = new DelegateCommand(LoadDB);
            AddCommand = new DelegateCommand(AddPeople);
            RemoveCommand = new DelegateCommand(RemovePeople, CanRemovePeople);
            UpdateCommand = new DelegateCommand(UpdatePeople);
            ImportCommand = new DelegateCommand(ImportCSV);
            FilterCommand = new DelegateCommand(FilterData);
        }

        private void LoadDB(object obj)
        {
            PeopleCollection = _peopleRepository.GetObjectsList();
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
            if (DateCheck.IsChecked == true && DateFilter.Length != 0 && DateFilter.Replace(" ","").Length != 0)
                SpecList.Add(new FindByDateSpecification(DateTime.Parse(DateFilter)));
            else
                SetBorderColor(mainWindow, "DateFilter");
            if (NameCheck.IsChecked == true && NameFilter.Length != 0 && NameFilter.Replace(" ","").Length != 0)
                SpecList.Add(new FindByNameSpecification(NameFilter));
            else
                SetBorderColor(mainWindow, "NameFilter");
            if (LNameCheck.IsChecked == true && LastNameFilter.Length != 0 && LastNameFilter.Replace(" ", "").Length != 0)
                SpecList.Add(new FindByLastNameSpecification(LastNameFilter));
            else
                SetBorderColor(mainWindow, "LastNameFilter");
            if (SNameCheck.IsChecked == true && SurNameFilter.Length != 0 && SurNameFilter.Replace(" ", "").Length != 0)
                SpecList.Add(new FindBySurNameSpecification(SurNameFilter));
            else
                SetBorderColor(mainWindow, "SurNameFilter");
            if (CityCheck.IsChecked == true && CityFilter.Length != 0 && CityFilter.Replace(" ", "").Length != 0)
                SpecList.Add(new FindByCitySpecification(CityFilter));
            else
                SetBorderColor(mainWindow, "CityFilter");
            if (CountryCheck.IsChecked == true && CountryFilter.Length != 0 && CountryFilter.Replace(" ", "").Length != 0)
                SpecList.Add(new FindByNameSpecification(CountryFilter));
            else
                SetBorderColor(mainWindow, "CountryFilter");
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
                MessageBox.Show("Не было выбрано ниодного условия фильтрации.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void SetBorderColor(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        private bool CanRemovePeople(object arg)
        {
            return (arg as People) != null;
        }

        //Insert/Update/Delete
        private void RemovePeople(object obj)
        {
            _peopleRepository.DeleteObject((People)obj);
            _peopleRepository.Save();
        }

        private void AddPeople(object obj)
        {
            if (AddDateOfBirth == null || AddDateOfBirth.Replace(" ", "").Length == 0)
                SetBorderColor(obj as Window, "AddDateOfBirth");
            else if (AddName == null || AddName.Replace(" ", "").Length == 0)
                SetBorderColor(obj as Window, "AddName");
            else if (AddLastName == null || AddLastName.Replace(" ", "").Length == 0)
                SetBorderColor(obj as Window, "AddLastName");
            else if (AddSurName == null || AddSurName.Replace(" ", "").Length == 0)
                SetBorderColor(obj as Window, "AddSurName");
            else if (AddCity == null || AddCity.Replace(" ", "").Length == 0)
                SetBorderColor(obj as Window, "AddCity");
            else if (AddCountry == null || AddCountry.Replace(" ", "").Length == 0)
                SetBorderColor(obj as Window, "AddCoutry");
            else
            {
                _peopleRepository.CreateObject(new People(DateTime.Parse(AddDateOfBirth).Date, AddName, AddLastName, AddSurName, AddCity, AddCountry));
                _peopleRepository.Save();
                ClearAddedPeople();
            }
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

        private void UpdatePeople(object obj)
        {
            _peopleRepository.UpdateObject((People)obj);
            SelectedPeople = null;
            _peopleRepository.Save();
        }
        //Insert/Update/Delete

        //Import/Export
        private async void ImportCSV(object obj)
        {
            var range = await _csvFileHandler.DataLoaderAsync(Environment.CurrentDirectory + @"\Import\persons.csv");
            foreach (var item in range)
            {
                _peopleRepository.CreateObject(item);
            }
            _peopleRepository.Save();
        }
        //Import/Export

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
