using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Models.Repositories
{
    public interface IMealRepository
    {
        IEnumerable<mealproduct> SelectAll();
        mealproduct SelectByID(int id);
        void Insert(mealproduct obj);
        void Update(mealproduct obj);
        void Delete(int id);
        void Save();
    }
}
