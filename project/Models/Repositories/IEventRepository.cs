using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Models.Repositories
{
   public interface IEventRepository
    {
        IEnumerable<_event> SelectAll();
        _event SelectByID(int id);
        void Insert(_event obj);
        void Update(_event obj);
        void Delete(int id);
        void Save();
    }
}
