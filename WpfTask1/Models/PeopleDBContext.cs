using System.Data.Entity;

namespace WpfTask1.Models
{
    class PeopleDBContext : DbContext
    {
        public PeopleDBContext() : base("DefaultConnection")
        {

        }
        public DbSet<People> People { get; set; }
    }
}
