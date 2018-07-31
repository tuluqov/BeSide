using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.BusinessLogic.Construct;
using BeSide.Common.Entities;
using BeSide.DataAccess.Construct;

namespace BeSide.BusinessLogic.BusinessComponents
{
    public class ServiceService : ISeviceService
    {
        private readonly IUnitOfWork uow;

        public ServiceService(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        public void AddService(Service service)
        {
            var newService = uow.Services.Find(m => m.Name == service.Name && m.Category.Name == service.Category.Name);

            if (newService == null)
            {
                uow.Services.Create(service);

                uow.Save();
            }
        }

        public void DeleteCategoty(Service service)
        {
            uow.Services.Delete(service.Id);
            uow.Save();
        }

        public IEnumerable<Service> GetAllService()
        {
            var allServices = uow.Services.GetAll().ToList();
            return allServices;
        }

        public void UpdateService(Service service)
        {
            uow.Services.Update(service);
        }
    }
}
