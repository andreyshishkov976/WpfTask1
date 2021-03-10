using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WpfTask1.Models;
using WpfTask1.Specifications;

namespace WpfTask1.Repositories
{
    class PeopleRepository : IRepository<People>
    {
        public PeopleRepository()
        {
            db = new PeopleDBContext();
        }

        internal PeopleDBContext db { get; set; }

        public async void LoadDB()
        {
            await db.People.LoadAsync();
        }

        public ICollection<People> GetObjectsList()
        {
            return db.People.Local;
        }

        public ICollection<People> GetObjectsList(string FilterParam)
        {
            return db.People.Local.Where(item => item.Name == FilterParam).ToList();
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
        public ICollection<People> Find(Specification<People> specification)
        {
            return db.People.Where(specification.ToExpression()).ToList();
        }

    }
}
