using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace project.Models.Repositories
{
    public class MedicationRepository : IMedicationRepository
    {
            private MedicationModelContext db = null;

            public MedicationRepository()
            {
                this.db = new MedicationModelContext();
            }

            public MedicationRepository(MedicationModelContext db)
            {
                this.db = db;
            }

            public void Delete(int id)
        {
            medication existing = db.medications.Find(id);
            db.medications.Remove(existing);

        }

        public void Insert(medication obj)
        {
            db.medications.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<medication> SelectAll()
        {
            return db.medications.ToList();
        }

        public medication SelectByID(int id)
        {
            return db.medications.Find(id);
        }

        public void Update(medication obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}