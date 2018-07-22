using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.DataAccess.Construct;

namespace BeSide.BusinessLogic.BusinessComponents
{
    class BaseService : IDisposable
    {
        protected IUnitOfWork uow;
        private bool _disposedValue;

        public BaseService(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    GC.SuppressFinalize(this);
                }

                uow?.Dispose();
                uow = null;
                _disposedValue = true;
            }
        }
    }
}
