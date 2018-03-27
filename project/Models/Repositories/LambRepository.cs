using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project.Models.Repositories
{
    public class LambRepository : ILambRepository
    {
        private LambDataContext db = null;

        public LambRepository()
        {
            this.db = new LambDataContext();
        }

        public LambRepository(LambDataContext db)
        {
            this.db = db;
        }

        public void Delete(int id)
        {
            lamb existing = db.lambs.Find(id);
            db.lambs.Remove(existing);
        }

        public void Insert(lamb obj)
        {
            db.lambs.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<lamb> SelectAll()
        {
            return db.lambs.ToList();
        }

        public lamb SelectByID(int id)
        {
            return db.lambs.Find(id);
        }

        public void Update(lamb obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}