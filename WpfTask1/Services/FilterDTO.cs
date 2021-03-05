namespace WpfTask1.Services
{
    class FilterDTO
    {
        private readonly string _dateOfBirthFilter;
        private readonly string _nameFilter;
        private readonly string _lastNameFilter;
        private readonly string _surNameFilter;
        private readonly string _cityFilter;
        private readonly string _countryFilter;

        public FilterDTO(string dateOfBirthFilter, string nameFilter, string lastNameFilter, string surNameFilter, string cityFilter, string countryFilter)
        {
            _dateOfBirthFilter = dateOfBirthFilter;
            _nameFilter = nameFilter;
            _lastNameFilter = lastNameFilter;
            _surNameFilter = surNameFilter;
            _cityFilter = cityFilter;
            _countryFilter = countryFilter;
        }

        public string DateOfBirthFilter => _dateOfBirthFilter;

        public string NameFilter => _nameFilter;

        public string LastNameFilter => _lastNameFilter;

        public string SurNameFilter => _surNameFilter;

        public string CityFilter => _cityFilter;

        public string CountryFilter => _countryFilter;
    }
}
