using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTask1.Models;

namespace WpfTask1.DataHandlers
{
    interface ICsvImporter<T> where T : class
    {
        Task<ICollection<T>> DataLoaderAsync(string path);
    }
}
