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
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork uow;

        public ImageService(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        public void Add(Image image)
        {
            uow.Images.Create(image);
            uow.Save();
        }

        public void Delete(int id)
        {
            uow.Images.Delete(id);
            uow.Save();
        }

        public IEnumerable<Image> Find(Func<Image, bool> predicate)
        {
            var result = uow.Images.Find(predicate);
            return result;
        }

        public IEnumerable<Image> GetAll()
        {
            var result = uow.Images.GetAll();
            return result;
        }

        public Image GetById(int id)
        {
            var result = uow.Images.GetById(id);
            return result;
        }

        public void Update(Image image)
        {
            uow.Images.Update(image);
            uow.Save();
        }
    }
}
