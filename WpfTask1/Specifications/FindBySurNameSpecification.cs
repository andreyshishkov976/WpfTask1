using System;
using System.Linq.Expressions;
using WpfTask1.Models;

namespace WpfTask1.Specifications
{
    class FindBySurNameSpecification : Specification<People>
    {
        private readonly string _surName;

        public FindBySurNameSpecification(string surName)
        {
            _surName = surName;
        }

        public override Expression<Func<People, bool>> ToExpression()
        {
            return people => people.SurName == _surName;
        }
    }
}
