using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Interfaces
{
    interface ICsvFileHandler
    {
        Task<ICollection<People>> DataLoaderAsync(string path);
    }
}
