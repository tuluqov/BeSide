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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork uow;

        public CategoryService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void AddCategory(Category category)
        {
            var newCategory = uow.Categories.Find(m => m.Name == category.Name).FirstOrDefault();

            if (newCategory == null)
            {
                uow.Categories.Create(category);

                uow.Save();
            }
        }

        public void DeleteCategory(Category category)
        {
            uow.Categories.Delete(category.Id);
            uow.Save();
        }

        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
            var findCategories = uow.Categories.Find(predicate);
            return findCategories;
        }

        public Category FindByName(Category category)
        {
            var findOrder = uow.Categories.Find(m => m.Name == category.Name).FirstOrDefault();
            return findOrder;
        }

        public IEnumerable<Category> GetAllCategory()
        {
            var allCategoty = uow.Categories.GetAll().ToList();
            return allCategoty;
        }

        public void UpdateCategory(Category category)
        {
            uow.Categories.Update(category);
            uow.Save();
        }
    }
}
