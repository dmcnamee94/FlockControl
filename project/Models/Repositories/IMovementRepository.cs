using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Models.Repositories
{
   public interface IMovementRepository
    {
        IEnumerable<movement> SelectAll();
        movement SelectByID(int id);
        void Insert(movement obj);
        void Update(movement obj);
        void Delete(int id);
        void Save();
    }
}
