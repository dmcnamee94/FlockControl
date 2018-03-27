using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project.Models.Repositories
{
    public class MealRepository : IMealRepository
    {
        private MealDataContext db = null;

        public MealRepository()
        {
            this.db = new MealDataContext();
        }

        public MealRepository(MealDataContext db)
        {
            this.db = db;
        }
        public void Delete(int id)
        {
            mealproduct existing = db.mealproducts.Find(id);
            db.mealproducts.Remove(existing);
        }

        public void Insert(mealproduct obj)
        {
            db.mealproducts.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(mealproduct obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        IEnumerable<mealproduct> IMealRepository.SelectAll()
        {
            return db.mealproducts.ToList();
        }

        mealproduct IMealRepository.SelectByID(int id)
        {
            return db.mealproducts.Find(id);
        }
    }

}