using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class People
    {
        public People()
        {
        }

        public People(string csvLine)
        {
            string[] values = csvLine.Split(';');
            DateOfBirth = values[0];
            Name = values[1];
            LastName = values[2];
            SurName = values[3];
            City = values[4];
            Country = values[5];
        }

        public People(int peopleId, string dateOfBirth, string name, string lastName, string surName, string city, string country)
        {
            PeopleId = peopleId;
            DateOfBirth = dateOfBirth;
            Name = name;
            LastName = lastName;
            SurName = surName;
            City = city;
            Country = country;
        }

        public int PeopleId { get; set; }
        public string DateOfBirth { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
