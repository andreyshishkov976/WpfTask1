using System;
using System.Linq.Expressions;
using WpfTask1.Models;

namespace WpfTask1.Specifications
{
    class FindByCitySpecification : Specification<People>
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
