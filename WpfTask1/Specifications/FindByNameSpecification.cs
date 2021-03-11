using System;
using System.Linq.Expressions;
using WpfTask1.Models;

namespace WpfTask1.Specifications
{
    class FindByNameSpecification : Specification<People>
    {
        private readonly string _name;

        public FindByNameSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<People, bool>> ToExpression()
        {
            return people => people.Name == _name;
        }
    }
}
