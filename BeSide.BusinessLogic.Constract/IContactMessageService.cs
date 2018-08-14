using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.Common.Entities;

namespace BeSide.BusinessLogic.Construct
{
    public interface IContactMessageService
    {
        void SendMessage(ContactMessage message);
        IEnumerable<ContactMessage> GetAll();
    }
}
