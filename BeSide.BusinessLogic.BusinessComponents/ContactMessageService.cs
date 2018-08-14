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
    public class ContactMessageService : IContactMessageService
    {
        private readonly IUnitOfWork uow;

        public ContactMessageService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public IEnumerable<ContactMessage> GetAll()
        {
            var result = uow.ContactMessages.GetAll();
            return result;
        }

        public void SendMessage(ContactMessage message)
        {
            uow.ContactMessages.Create(message);
            uow.Save();
        }
    }
}
