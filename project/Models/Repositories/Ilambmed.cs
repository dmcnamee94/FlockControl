using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Models.Repositories
{
   public  interface Ilambmed
    {
        IEnumerable<lambmed> SelectAll();
        lambmed SelectByID(int id);
        void Insert(lambmed obj);
        void Update(lambmed obj);
        void Delete(int id);
        void Save();
    }
}
