using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace project.Models.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserDataContext db = null;

        public UserRepository()
        {
            this.db = new UserDataContext();
        }

        public UserRepository(UserDataContext db)
        {
            this.db = db;
        }


        public void Delete(int id)
        {
            usertable existing = db.usertables.Find(id);
            db.usertables.Remove(existing);
        }

        public void Insert(usertable obj)
        {
            db.usertables.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<usertable> SelectAll()
        {
            return db.usertables.ToList();
        }

        public usertable SelectByID(int id)
        {
            return db.usertables.Find(id);
        }

        public void Update(usertable obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}