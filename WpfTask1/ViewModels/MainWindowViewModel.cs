using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using WpfTask1.DataHandlers;
using WpfTask1.Interfaces;
using WpfTask1.Models;

namespace WpfTask1.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
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

        //Added People
        private string _dateOfBirth;
        public string DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        //Added People

        private IRepository<People> _peopleRepository;
        private ICsvImporter<People> _csvFileHandler;
        public ICollection<People> PeopleCollection { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public MainWindowViewModel()
        {
            PeopleCollection = new ObservableCollection<People>();
            _peopleRepository = new PeopleRepository();
            _csvFileHandler = new CSVPeopleImporter();
            PeopleCollection = _peopleRepository.GetObjectsList();
            AddCommand = new DelegeteCommand(AddPeople);
            RemoveCommand = new DelegeteCommand(RemovePeople, CanRemovePeople);
            ImportCommand = new DelegeteCommand(ImportCSV);
            SaveCommand = new DelegeteCommand(SaveChanges);
        }

        private bool CanRemovePeople(object arg)
        {
            return (arg as People) != null;
        }

        private void RemovePeople(object obj)
        {
            _peopleRepository.DeleteObject((People)obj);
            _peopleRepository.Save();
        }

        private void AddPeople(object obj)
        {
            _peopleRepository.CreateObject(new People(DateTime.Parse(DateOfBirth).Date, Name, LastName, SurName, City, Country));
            _peopleRepository.Save();
            DateOfBirth = string.Empty;
            Name = string.Empty;
            LastName = string.Empty;
            SurName = string.Empty;
            City = string.Empty;
            Country = string.Empty;
        }
        private void SaveChanges(object obj)
        {
            _peopleRepository.UpdateObject((People)obj);
            SelectedPeople = null;
            _peopleRepository.Save();
        }
        private async void ImportCSV(object obj)
        {
            var range = await _csvFileHandler.DataLoaderAsync(Environment.CurrentDirectory + @"\Import\persons.csv");
            foreach (var item in range)
            {
                _peopleRepository.CreateObject(item);
            }
            _peopleRepository.Save();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
