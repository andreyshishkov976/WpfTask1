using System;
using System.Linq.Expressions;
using WpfTask1.Models;

namespace WpfTask1.Specifications
{
    class FindByCountrySpecification : Specification<People>
    {
        private readonly string _city;

        public FindByCountrySpecification(string city)
        {
            _city = city;
        }

        public override Expression<Func<People, bool>> ToExpression()
        {
            return people => people.LastName == _city;
        }
    }
}
