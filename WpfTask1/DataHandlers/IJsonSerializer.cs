using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTask1.Models;

namespace WpfTask1.DataHandlers
{
    interface IJsonSerializer<T> where T : class
    {
        void Serialize(ICollection<T> people);
    }
}
