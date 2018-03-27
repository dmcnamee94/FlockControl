using project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project.Models.Repositories
{
    public class MealSupplierRepository : IMealSupplierRepository
    {
        private MealSupplierDataContext db = null;

        public MealSupplierRepository()
        {
            this.db = new MealSupplierDataContext();
        }

        public MealSupplierRepository(MealSupplierDataContext db)
        {
            this.db = db;
        }


        public void Delete(int id)
        {
            mealsupplier existing = db.mealsuppliers.Find(id);
            db.mealsuppliers.Remove(existing);
        }

        public void Insert(mealsupplier obj)
        {
            db.mealsuppliers.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<mealsupplier> SelectAll()
        {
            return db.mealsuppliers.ToList();
        }

        public mealsupplier SelectByID(int id)
        {
            return db.mealsuppliers.Find(id);
        }

        public void Update(mealsupplier obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}