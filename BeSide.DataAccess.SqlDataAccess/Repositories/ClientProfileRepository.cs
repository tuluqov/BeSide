using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.Common.Entities;
using BeSide.DataAccess.Construct;

namespace BeSide.DataAccess.SqlDataAccess.Repositories
{
    public class ClientProfileRepository : IClientProfileRepository
    {
        private readonly DbContext context;
        private readonly DbSet<ClientProfile> profiles;

        public ClientProfileRepository(DbContext context)
        {
            this.context = context;
            profiles = context.Set<ClientProfile>();
        }

        public ClientProfile Create(ClientProfile item)
        {
            profiles.Add(item);
            context.SaveChanges();
            return item;
        }

        public ClientProfile Delete(string id)
        {
            ClientProfile resault = profiles.Find(id);

            if (resault != null)
            {
                profiles.Remove(resault);
            }

            return resault;
        }

        public IEnumerable<ClientProfile> Find(Func<ClientProfile, bool> predicate)
        {
            IEnumerable<ClientProfile> result = profiles.Where(predicate).ToList();
            return result;
        }

        public IEnumerable<ClientProfile> GetAll()
        {
            return profiles.ToList();
        }

        public ClientProfile GetById(string id)
        {
            var result = profiles.Find(id);
            return result;
        }

        public ClientProfile Update(ClientProfile item)
        {
            context.Entry(item).State = EntityState.Modified;
            return item;
        }
    }
}
