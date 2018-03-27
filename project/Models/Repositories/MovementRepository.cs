using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project.Models.Repositories
{
    public class MovementRepository : IMovementRepository
    {
        private MovementDataContext db = null;

        public MovementRepository()
        {
            this.db = new MovementDataContext();
        }

        public MovementRepository(MovementDataContext db)
        {
            this.db = db;
        }
        public void Delete(int id)
        {
            movement existing = db.movements.Find(id);
            db.movements.Remove(existing);

        }

        public void Insert(movement obj)
        {
            db.movements.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<movement> SelectAll()
        {
           return db.movements.ToList();
        }

        public movement SelectByID(int id)
        {
            return db.movements.Find(id);
        }

        public void Update(movement obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}