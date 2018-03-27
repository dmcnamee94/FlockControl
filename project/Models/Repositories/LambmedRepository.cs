using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project.Models.Repositories
{
    public class LambmedRepository : Ilambmed
    {
        private LambMedDataContext db = null;

        public LambmedRepository()
        {
            this.db = new LambMedDataContext();
        }

        public LambmedRepository(LambMedDataContext db)
        {
            this.db = db;
        }

        public void Delete(int id)
        {
            lambmed existing = db.lambmeds.Find(id);
            db.lambmeds.Remove(existing);
        }

        public void Insert(lambmed obj)
        {
            db.lambmeds.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<lambmed> SelectAll()
        {
            return db.lambmeds.ToList();
        }

        public lambmed SelectByID(int id)
        {
            return db.lambmeds.Find(id);
        }

        public void Update(lambmed obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}