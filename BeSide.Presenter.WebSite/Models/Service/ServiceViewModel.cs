using System.ComponentModel.DataAnnotations;

namespace BeSide.Presenter.WebSite.Models.Service
{
    public class ServiceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        public int CategoryId { get; set; }


        public ServiceViewModel()
        {

        }

        public ServiceViewModel(Common.Entities.Service service)
        {
            Id = service.Id;
            Name = service.Name;
            CategoryId = service.CategoryId;
        }

        public void UpdateService(Common.Entities.Service service)
        {
            service.Name = Name;
        }

        public Common.Entities.Service GetService()
        {
            return new Common.Entities.Service
            {
                CategoryId = CategoryId,
                Id = Id,
                Name = Name
            };
        }
    }
}