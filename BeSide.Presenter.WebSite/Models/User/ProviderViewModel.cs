using System.ComponentModel.DataAnnotations;
using BeSide.Common.Entities;
using BeSide.Presenter.WebSite.Models.Category;
using BeSide.Presenter.WebSite.Models.Feedback;
using BeSide.Presenter.WebSite.Models.Service;

namespace BeSide.Presenter.WebSite.Models.User
{
    public class ProviderViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Patronymic { get; set; }

        [MaxLength(100)]
        public string CompanyName { get; set; }

        [MaxLength(300)]
        public string Discription { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public ServiceCollectionViewModel Services { get; set; }

        public CategoryCollectionViewModel Categories { get; set; }

        public FeedbacksCollectionViewModel Feedbacks { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public FileType FileType { get; set; }

        public ProviderViewModel()
        {

        }

        public ProviderViewModel(ProviderProfile profile)
        {
            Id = profile.Id;
            FirstName = profile.FirstName;
            LastName = profile.LastName;
            Patronymic = profile.Patronymic;
            CompanyName = profile.CompanyName;
            ApplicationUser = profile.ApplicationUser;
            Discription = profile.Discription;
            Services = new ServiceCollectionViewModel(profile.Services);
            Feedbacks = new FeedbacksCollectionViewModel(profile.Feedbacks);
            Categories = new CategoryCollectionViewModel(profile.Categories);
            ContentType = profile.ContentType;
            Content = profile.Content;
            FileName = profile.FileName;
            FileType = profile.FileType;
        }

        public ProviderProfile GProfile()
        {
            return new ProviderProfile
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Patronymic = Patronymic,
                CompanyName = CompanyName,
                Discription = Discription,
                ContentType = ContentType,
                Content = Content,
                FileName = FileName,
                FileType = FileType
            };
        }
    }
}