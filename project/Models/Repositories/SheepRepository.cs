using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace project.Models.Repositories
{
    public class SheepRepository : ISheepRepository

    {
        private DBContext db = null;

        public SheepRepository()
        {
            this.db = new DBContext();
        }

        public SheepRepository(DBContext db)
        {
            this.db = db;
        }

        public void Delete(int id)
        {
            sheep existing = db.sheep.Find(id);
            db.sheep.Remove(existing);
        }

        public void Insert(sheep obj)
        {
           db.sheep.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<sheep> SelectAll()
        {
            return db.sheep.ToList();
        }

        public sheep SelectByID(int id)
        {
            return db.sheep.Find(id);
        }

        public void Update(sheep obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}