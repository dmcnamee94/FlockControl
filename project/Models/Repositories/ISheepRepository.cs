using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace project.Models.Repositories
{
    public interface ISheepRepository
    {
        IEnumerable<sheep> SelectAll();
        sheep SelectByID(int id);
        void Insert(sheep obj);
        void Update(sheep obj);
        void Delete(int id);
        void Save();
    }
}
