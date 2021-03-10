using System;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfTask1.Models
{
    public class People : INotifyPropertyChanged
    {
        private int _peopleId;
        private DateTime _dateOfBirth;
        private string _name;
        private string _lastName;
        private string _surName;
        private string _city;
        private string _country;

        public int PeopleId { get { return _peopleId; } set { _peopleId = value; OnPropertyChanged("PeopleId"); } }
        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get { return _dateOfBirth; } set { _dateOfBirth = value; OnPropertyChanged("DateOfBirth"); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public string LastName { get { return _lastName; } set { _lastName = value; OnPropertyChanged("LastName"); } }
        public string SurName { get { return _surName; } set { _surName = value; OnPropertyChanged("SurName"); } }
        public string City { get { return _city; } set { _city = value; OnPropertyChanged("City"); } }
        public string Country { get { return _country; } set { _country = value; OnPropertyChanged("Country"); } }

        public People()
        {
        }

        public People(string csvLine)
        {
            string[] values = csvLine.Split(';');
            DateOfBirth = DateTime.Parse(values[0]).Date;
            Name = values[1];
            LastName = values[2];
            SurName = values[3];
            City = values[4];
            Country = values[5];
        }

        public People(DateTime dateOfBirth, string name, string lastName, string surName, string city, string country)
        {
            DateOfBirth = dateOfBirth.Date;
            Name = name;
            LastName = lastName;
            SurName = surName;
            City = city;
            Country = country;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
