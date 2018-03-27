using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project.Models.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private ResultDataContext db = null;

        public ResultRepository()
        {
            this.db = new ResultDataContext();
        }

        public ResultRepository(ResultDataContext db)
        {
            this.db = db;
        }

        public void Delete(int id)
        {
            result existing = db.results.Find(id);
            db.results.Remove(existing);
        }

        public void Insert(result obj)
        {
            db.results.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<result> SelectAll()
        {
            return db.results.ToList();
        }

        public result SelectByID(int id)
        {
            return db.results.Find(id);
        }

        public void Update(result obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}