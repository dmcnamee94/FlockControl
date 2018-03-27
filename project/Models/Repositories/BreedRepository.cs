using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project.Models.Repositories
{
    public class BreedRepository : IBreedRepository
    {
        private BreedDataContext db = null;

        public BreedRepository()
        {
            this.db = new BreedDataContext();
        }

        public BreedRepository(BreedDataContext db)
        {
            this.db = db;
        }

        public void Delete(int id)
        {
            breed existing = db.breeds.Find(id);
            db.breeds.Remove(existing);
        }

        public void Insert(breed obj)
        {
            db.breeds.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<breed> SelectAll()
        {
            return db.breeds.ToList();
        }

        public breed SelectByID(int id)
        {
            return db.breeds.Find(id);
        }

        public void Update(breed obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}