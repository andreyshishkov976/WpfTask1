using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task LoadDB()
        {
            await db.People.LoadAsync();
        }

        public async Task<ICollection<People>> GetObjectsList()
        {
            return await db.People.ToListAsync();
        }

        public People GetObject(int id)
        {
            return db.People.Find(id);
        }

        public async Task<ICollection<People>> CreateObject(People item)
        {
            db.People.Add(item);
            await db.SaveChangesAsync();
            return await db.People.ToListAsync();
        }
        public async Task<ICollection<People>> CreateRange(ICollection<People> range)
        {
            db.People.AddRange(range);
            await db.SaveChangesAsync();
            return await db.People.ToListAsync();
        }

        public async Task<ICollection<People>> DeleteObject(People item)
        {
            bool oldValidateOnSaveEnabled = db.Configuration.ValidateOnSaveEnabled;
            try
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                var people = new People { PeopleId = item.PeopleId };
                db.People.Attach(people);
                db.Entry(people).State = EntityState.Deleted;
                await db.SaveChangesAsync();
            }
            finally
            {
                db.Configuration.ValidateOnSaveEnabled = oldValidateOnSaveEnabled;
            }
            return await db.People.ToListAsync();
            //if (db.People.Find(item.PeopleId) != null)
            //    db.People.Remove(item);
            //    //db.Entry(item).State = EntityState.Deleted;
            //await db.SaveChangesAsync();
        }

        public async Task<ICollection<People>> UpdateObject(People item)
        {
            db.Entry(item).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return await db.People.ToListAsync();
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

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public async Task<ICollection<People>> Find(Specification<People> specification)
        {
            return await db.People.Where(specification.ToExpression()).ToListAsync();
        }

    }
}
