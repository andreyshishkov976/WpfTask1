using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfTask1.Models;

namespace WpfTask1.Specifications
{
    class FindByCountrySpecification: Specification<People>
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
