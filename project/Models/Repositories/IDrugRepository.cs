using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Models.Repositories
{
    public interface IDrugRepository
    {
        IEnumerable<drug> SelectAll();
        drug SelectByID(int id);
        void Insert(drug obj);
        void Update(drug obj);
        void Delete(int id);
        void Save();
    }
}
