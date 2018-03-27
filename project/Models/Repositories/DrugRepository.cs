using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project.Models.Repositories
{
    public class DrugRepository : IDrugRepository
    {
        private DrugDataContext db = null;

        public DrugRepository()
        {
            this.db = new DrugDataContext();
        }

        public DrugRepository(DrugDataContext db)
        {
            this.db = db;
        }

        public void Delete(int id)
        {
            drug existing = db.drugs.Find(id);
            db.drugs.Remove(existing);
        }

        public void Insert(drug obj)
        {
            db.drugs.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<drug> SelectAll()
        {
            return db.drugs.ToList();
        }

        public drug SelectByID(int id)
        {
            return db.drugs.Find(id);
        }

        public void Update(drug obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}