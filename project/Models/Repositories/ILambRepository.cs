using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Models.Repositories
{
    public interface ILambRepository
    {
        IEnumerable<lamb> SelectAll();
        lamb SelectByID(int id);
        void Insert(lamb obj);
        void Update(lamb obj);
        void Delete(int id);
        void Save();
    }
}
