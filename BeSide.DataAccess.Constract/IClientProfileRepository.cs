using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.Common.Entities;

namespace BeSide.DataAccess.Construct
{
    public interface IClientProfileRepository
    {
        ClientProfile Create(ClientProfile item);
        ClientProfile Update(ClientProfile item);
        ClientProfile Delete(string id);
        ClientProfile GetById(string id);
        IEnumerable<ClientProfile> GetAll();
        IEnumerable<ClientProfile> Find(Func<ClientProfile, Boolean> predicate);
    }
}
