using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace project.Models.Repositories
{
    public interface IMedicationRepository
    {

        IEnumerable<medication> SelectAll();
        medication SelectByID(int id);
        void Insert(medication obj);
        void Update(medication obj);
        void Delete(int id);
        void Save();

    }
}
