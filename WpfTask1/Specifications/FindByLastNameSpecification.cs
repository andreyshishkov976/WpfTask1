using System;
using System.Linq.Expressions;
using WpfTask1.Models;

namespace WpfTask1.Specifications
{
    class FindByLastNameSpecification : Specification<People>
    {
        private readonly string _lastName;

        public FindByLastNameSpecification(string lastName)
        {
            _lastName = lastName;
        }

        public override Expression<Func<People, bool>> ToExpression()
        {
            return people => people.LastName == _lastName;
        }
    }
}
