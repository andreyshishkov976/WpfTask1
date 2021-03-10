using System;
using System.Collections.Generic;
using WpfTask1.Specifications;

namespace WpfTask1.Repositories
{
    interface IRepository<T> : IDisposable
        where T : class
    {
        void LoadDB();
        ICollection<T> GetObjectsList();
        ICollection<T> Find(Specification<T> specification);
        T GetObject(int id);
        void CreateObject(T item);
        void DeleteObject(T item);
        void UpdateObject(T item);
        void Save();
    }
}
