using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfTask1.DataHandlers
{
    interface ICsvImporter<T> where T : class
    {
        Task<ICollection<T>> DataLoaderAsync(string path);
    }
}
