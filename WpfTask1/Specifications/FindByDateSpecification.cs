using System;
using System.Linq.Expressions;
using WpfTask1.Models;

namespace WpfTask1.Specifications
{
    class FindByDateSpecification : Specification<People>
    {
        private readonly DateTime _date;

        public FindByDateSpecification(DateTime date)
        {
            _date = date;
        }

        public override Expression<Func<People, bool>> ToExpression()
        {
            return people => people.DateOfBirth == _date;
        }
    }
}
