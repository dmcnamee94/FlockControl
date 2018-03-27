using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Models.Repositories
{
    public interface ICalEvent
    {
        IEnumerable<calevent> SelectAll();
        calevent SelectByID(int id);
        void Insert(calevent obj);
        void Update(calevent obj);
        void Delete(int id);
        void Save();
    }
}
