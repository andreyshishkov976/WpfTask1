using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Person
    {
        public Person(string csvLine)
        {
            string[] values = csvLine.Split(';');
            DateOfBirth = values[0];
            Name = values[1];
            LastName = values[2];
            SoName = values[3];
            City = values[4];
            Country = values[5];
        }

        private string _dateOfBirth;
        private string _name;
        private string _lastName;
        private string _soName;
        private string _city;
        private string _country;

        public string DateOfBirth { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SoName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
