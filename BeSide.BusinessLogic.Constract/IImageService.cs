using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.Common.Entities;

namespace BeSide.BusinessLogic.Construct
{
    public interface IImageService
    {
        IEnumerable<Image> GetAll();
        Image GetById(int id);
        void Add(Image image);
        void Delete(int id);
        void Update(Image image);
        IEnumerable<Image> Find(Func<Image, bool> predicate);
    }
}
