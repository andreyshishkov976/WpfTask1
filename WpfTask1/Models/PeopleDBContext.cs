using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Models
{
    class PeopleDBContext: DbContext
    {
        public PeopleDBContext() : base("DefaultConnection")
        {

        }
        public DbSet<People> People { get; set; }
    }
}
