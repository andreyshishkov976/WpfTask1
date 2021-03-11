using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfTask1.Specifications;

namespace WpfTask1.Repositories
{
    interface IRepository<T> : IDisposable
        where T : class
    {
        Task LoadDB();
        Task<ICollection<T>> GetObjectsList();
        Task<ICollection<T>> Find(Specification<T> specification);
        T GetObject(int id);
        Task<ICollection<T>> CreateObject(T item);
        Task<ICollection<T>> CreateRange(ICollection<T> range);
        Task<ICollection<T>> DeleteObject(T item);
        Task<ICollection<T>> UpdateObject(T item);
        Task SaveAsync();
    }
}
