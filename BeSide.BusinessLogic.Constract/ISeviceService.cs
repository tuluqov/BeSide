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
        void DeleteCategoty(Service service);
        void UpdateService(Service service);
        IEnumerable<Service> GetAllService();
    }
}
