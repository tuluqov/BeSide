using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.Common.Entities;

namespace BeSide.BusinessLogic.Construct
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        void DeleteCategory(Category category);
        void DeleteById(int id);
        void UpdateCategory(Category category);
        IEnumerable<Category> GetAllCategory();
        IEnumerable<Category> Find(Func<Category, bool> predicate);
        Category FindByName(string name);
        Category GetById(int id);
    }
}
