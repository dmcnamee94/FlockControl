using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Models.Repositories
{
    public interface IDrugSupplierRepository
    {
        IEnumerable<drugsupplier> SelectAll();
        drugsupplier SelectByID(int id);
        void Insert(drugsupplier obj);
        void Update(drugsupplier obj);
        void Delete(int id);
        void Save();
        drugsupplier SelectByID(string supplierName);
    }
}
