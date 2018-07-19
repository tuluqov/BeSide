using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.Common.Entities;

namespace BeSide.DataAccess.Construct
{
    public interface IClientManager : IDisposable
    {
        void Create(User item);
    }
}
