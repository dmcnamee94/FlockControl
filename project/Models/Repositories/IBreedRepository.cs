using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Models.Repositories
{
    public interface IBreedRepository
    {
        IEnumerable<breed> SelectAll();
        breed SelectByID(int id);
        void Insert(breed obj);
        void Update(breed obj);
        void Delete(int id);
        void Save();
    }
}
