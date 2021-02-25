using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Task3.Models;

namespace Task3.Interfaces
{
    interface IDataBaseHandler
    {
        Task ImportPeopleData(ICollection<People> people);
        void FilterPeopleData();
        Task<IEnumerable<People>> LoadPeopleTable();
        void DisposeConnection();
    }
}
