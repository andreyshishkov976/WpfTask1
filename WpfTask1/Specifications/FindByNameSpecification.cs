using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfTask1.Models;

namespace WpfTask1.Specifications
{
    class FindByNameSpecification: Specification<People>
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
