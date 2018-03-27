using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace project.Models.Repositories
{
   public interface INewUserRepository
    {

        IEnumerable<usertable> SelectAll();
        usertable SelectByID(int id);
        void Insert(usertable obj);
        void Update(usertable obj);
        void Delete(int id);
        void Save();

    }
}
