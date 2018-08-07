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
            var newService = uow.Services.Find(m => m.Name == service.Name && m.CategoryId == service.CategoryId).FirstOrDefault();

            if (newService == null)
            {
                uow.Services.Create(service);

                uow.Save();
            }
        }

        public void DeleteById(int id)
        {
            uow.Services.Delete(id);
            uow.Save();
        }

        public void Delete(Service service)
        {
            uow.Services.Delete(service.Id);
            uow.Save();
        }

        public IEnumerable<Service> Find(Func<Service, bool> predicate)
        {
            var result = uow.Services.Find(predicate);
            return result;
        }

        public Service FindByName(string nameService, string nameCategory)
        {
            Category category = uow.Categories.Find(m => m.Name == nameCategory).FirstOrDefault();


            return uow.Services.Find(m => m.Name == nameService && m.Category.Name == nameCategory)
                .FirstOrDefault();
        }

        public IEnumerable<Service> GetAllService()
        {
            var allServices = uow.Services.GetAll().ToList();
            return allServices;
        }

        public Service GetById(int id)
        {
            var result = uow.Services.GetById(id);
            return result;
        }

        public void UpdateService(Service service)
        {
            uow.Services.Update(service);
        }
    }
}
