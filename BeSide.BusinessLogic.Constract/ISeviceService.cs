using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.Common.Entities;

namespace BeSide.BusinessLogic.Construct
{
    public interface ISeviceService
    {
        void AddService(Service service);
        void Delete(Service service);
        void DeleteById(int id);
        void UpdateService(Service service);
        Service FindByName(string nameService, string nameCategory);
        Service GetById(int id);
        IEnumerable<Service> GetAllService();
        IEnumerable<Service> Find(Func<Service, bool> predicate);
    }
}
