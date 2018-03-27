using project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project.Models.Repositories
{
    public class DrugSupplierRepository : IDrugSupplierRepository
    {
        private DrugSuplierDataContext db = null;

        public DrugSupplierRepository()
        {
            this.db = new DrugSuplierDataContext();
        }

        public DrugSupplierRepository(DrugSuplierDataContext db)
        {
            this.db = db;
        }


        public void Delete(int id)
        {
            drugsupplier existing = db.drugsuppliers.Find(id);
            db.drugsuppliers.Remove(existing);
        }

        public void Insert(drugsupplier obj)
        {
            db.drugsuppliers.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<drugsupplier> SelectAll()
        {
            return db.drugsuppliers.ToList();
        }

        public drugsupplier SelectByID(string supplierName)
        {
            throw new NotImplementedException();
        }

        public drugsupplier SelectByID(int id)
        {
            return db.drugsuppliers.Find(id);
        }

        public void Update(drugsupplier obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}