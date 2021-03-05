using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfTask1.Models;

namespace WpfTask1.Specifications
{
    class FindByCitySpecification: Specification<People>
    {
        private readonly string _city;

        public FindByCitySpecification(string city)
        {
            _city = city;
        }

        public override Expression<Func<People, bool>> ToExpression()
        {
            return people => people.City == _city;
        }
    }
}
