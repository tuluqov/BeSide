using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BeSide.Presenter.WebSite.Models.Service;

namespace BeSide.Presenter.WebSite.Models.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        public ServiceCollectionViewModel ServiceViewModels { get; set; }

        public CategoryViewModel()
        {
            
        }

        public CategoryViewModel(Common.Entities.Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ServiceViewModels = new ServiceCollectionViewModel(category.Services);
        }
    }
}