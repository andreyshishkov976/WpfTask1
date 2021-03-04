using System;
using System.Collections.Generic;

namespace WpfTask1.Interfaces
{
    interface IRepository<T>:IDisposable
        where T:class
    {
        ICollection<T> GetObjectsList();
        T GetObject(int id);
        void CreateObject(T item);
        void DeleteObject(T item);
        void UpdateObject(T item);
        void Save();
    }
}
