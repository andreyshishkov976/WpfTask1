using System;
using System.Collections.Generic;
using System.Data.Entity;
using WpfTask1.Interfaces;
using WpfTask1.Models;

namespace WpfTask1.DataHandlers
{
    class PeopleRepository : IRepository<People>
    {
        public PeopleRepository()
        {
            db = new PeopleDBContext();
            db.People.LoadAsync();
        }

        internal PeopleDBContext db { get; set; }

        public ICollection<People> GetObjectsList()
        {
            return db.People.Local;
        }

        public People GetObject(int id)
        {
            return db.People.Find(id);
        }

        public void CreateObject(People item)
        {
            db.People.Add(item);
        }

        public void DeleteObject(People item)
        {
            if (db.People.Find(item.PeopleId) != null)
                db.People.Remove(item);
        }

        public void UpdateObject(People item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async void Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
