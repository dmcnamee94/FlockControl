using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace project.Models.Repositories
{
    public class EventRepository : IEventRepository
    {
        private EventDataContext db = null;

        public EventRepository()
        {
            this.db = new EventDataContext();
        }

        public EventRepository(EventDataContext db)
        {
            this.db = db;
        }
        public void Delete(int id)
        {
            _event existing = db.events.Find(id);
            db.events.Remove(existing);
        }

        public void Insert(_event obj)
        {
            db.events.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<_event> SelectAll()
        {
            return db.events.ToList();
        }

        public _event SelectByID(int id)
        {
            return db.events.Find(id);
        }

        public void Update(_event obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}