using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Models.Repositories
{
    public interface IResultRepository
    {
        IEnumerable<result> SelectAll();
        result SelectByID(int id);
        void Insert(result obj);
        void Update(result obj);
        void Delete(int id);
        void Save();
    }
}
