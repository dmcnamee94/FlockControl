using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Models.Repositories
{
    public interface IMealSupplierRepository
    {
        IEnumerable<mealsupplier> SelectAll();
        mealsupplier SelectByID(int id);
        void Insert(mealsupplier obj);
        void Update(mealsupplier obj);
        void Delete(int id);
        void Save();
    }
}
