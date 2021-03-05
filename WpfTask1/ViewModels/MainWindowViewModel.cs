﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using WpfTask1.DataHandlers;
using WpfTask1.Interfaces;
using WpfTask1.Models;
using WpfTask1.Services;

namespace WpfTask1.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        Filter filter;
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
        public string DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }
        public string Name {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string LastName {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public string SurName {
            get { return _surName; }
            set
            {
                _surName = value;
                OnPropertyChanged("SurName");
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }
        public string Country {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged("Country");
            }
        }
        //Added People

        ////Filter People
        //private string _dateOfBirthFilter;
        //private string _nameFilter;
        //private string _lastNameFilter;
        //private string _surNameFilter;
        //private string _cityFilter;
        //private string _countryFilter;
        //public string DateOfBirthFilter
        //{
        //    get { return _dateOfBirthFilter; }
        //    set
        //    {
        //        _dateOfBirthFilter = value;
        //        OnPropertyChanged("DateOfBirthFilter");
        //    }
        //}
        //public string NameFilter
        //{
        //    get { return _nameFilter; }
        //    set
        //    {
        //        _nameFilter = value;
        //        OnPropertyChanged("NameFilter");
        //    }
        //}
        //public string LastNameFilter
        //{
        //    get { return _lastNameFilter; }
        //    set
        //    {
        //        _lastNameFilter = value;
        //        OnPropertyChanged("LastNameFilter");
        //    }
        //}
        //public string SurNameFilter
        //{
        //    get { return _surNameFilter; }
        //    set
        //    {
        //        _surNameFilter = value;
        //        OnPropertyChanged("SurNameFilter");
        //    }
        //}
        //public string CityFilter
        //{
        //    get { return _cityFilter; }
        //    set
        //    {
        //        _cityFilter = value;
        //        OnPropertyChanged("CityFilter");
        //    }
        //}
        //public string CountryFilter
        //{
        //    get { return _countryFilter; }
        //    set
        //    {
        //        _countryFilter = value;
        //        OnPropertyChanged("CountryFilter");
        //    }
        //}
        ////Filter People

        private IRepository<People> _peopleRepository;
        private ICsvImporter<People> _csvFileHandler;
        public ICollection<People> PeopleCollection { get; set; }

        //Commands
        public ICommand LoadCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        //public ICommand FilterCommand { get; private set; }
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
            //FilterCommand = new DelegateCommand(FilterData);
            //filter = new Filter();
        }

        private void LoadDB(object obj)
        {
            PeopleCollection = _peopleRepository.GetObjectsList();
        }

        private void FilterData(object obj)
        {

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
            _peopleRepository.CreateObject(new People(DateTime.Parse(DateOfBirth).Date, Name, LastName, SurName, City, Country));
            _peopleRepository.Save();
            ClearAddedPeople();
        }

        private void ClearAddedPeople()
        {
            DateOfBirth = string.Empty;
            Name = string.Empty;
            LastName = string.Empty;
            SurName = string.Empty;
            City = string.Empty;
            Country = string.Empty;
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