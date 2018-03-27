using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project.Models.Repositories
{
    public class CalEventRepository : ICalEvent
    {
        private CalEventContext db = null;

        public CalEventRepository()
        {
            this.db = new CalEventContext();
        }

        public CalEventRepository(CalEventContext db)
        {
            this.db = db;
        }
        public void Delete(int id)
        {
            calevent existing = db.calevents.Find(id);
            db.calevents.Remove(existing);
        }

        public void Insert(calevent obj)
        {
            db.calevents.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<calevent> SelectAll()
        {
            return db.calevents.ToList();
        }

        public calevent SelectByID(int id)
        {
            return db.calevents.Find(id);
        }

        public void Update(calevent obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

      
    }
}