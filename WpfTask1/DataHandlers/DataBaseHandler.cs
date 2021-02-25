using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Task3.Interfaces;
using Task3.Models;

namespace Task3.DataHandlers
{
    class DataBaseHandler : IDataBaseHandler
    {
        public DataBaseHandler()
        {
            PeopleDBContext = new PeopleDBContext();
        }

        internal PeopleDBContext PeopleDBContext { get; set; }

        public void DisposeConnection()
        {
            PeopleDBContext.Dispose();
        }

        public void FilterPeopleData()
        {
            throw new NotImplementedException();
        }

        public async Task ImportPeopleData(ICollection<People> people)
        {
            PeopleDBContext.People.AddRange(people);
            await PeopleDBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<People>> LoadPeopleTable()
        {
            await Task.Run(() => PeopleDBContext.People.Load());
            return PeopleDBContext.People.Local.ToBindingList();
        }
    }
}
