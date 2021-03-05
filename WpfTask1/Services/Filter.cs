using System;
using System.Collections.Generic;
using System.Linq;
using WpfTask1.Models;

namespace WpfTask1.Services
{
    class Filter
    {
        public ICollection<People> DoFilter(ICollection<People> collection, FilterDTO param)
        {
            IEnumerable<People> result = collection;
            if (param.DateOfBirthFilter != null && param.DateOfBirthFilter != string.Empty)
                result = result.Where(item => item.DateOfBirth == DateTime.Parse(param.DateOfBirthFilter).Date);
            if (param.NameFilter != null && param.NameFilter != string.Empty)
                result = result.Where(item => item.Name == param.NameFilter);
            if (param.LastNameFilter != null && param.LastNameFilter != string.Empty)
                result = result.Where(item => item.LastName == param.LastNameFilter);
            if (param.SurNameFilter != null && param.SurNameFilter != string.Empty)
                result = result.Where(item => item.SurName == param.SurNameFilter);
            if (param.CityFilter != null && param.CityFilter != string.Empty)
                result = result.Where(item => item.City == param.CityFilter);
            if (param.CountryFilter != null && param.CountryFilter != string.Empty)
                result = result.Where(item => item.Country == param.CountryFilter);
            ICollection<People> finish = result.ToList();
            return finish;
        }
    }
}
