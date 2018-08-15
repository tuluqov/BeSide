using System.ComponentModel.DataAnnotations;
using BeSide.Common.Entities;

namespace BeSide.Presenter.WebSite.Models.User
{
    public class EditProviderProfileViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Имя")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        [MaxLength(50)]
        public string Patronymic { get; set; }

        [Required]
        [Display(Name = "Номер телефона")]
        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        [Display(Name = "УНП")]
        public string CompanyName { get; set; }

        [MaxLength(300)]
        [Display(Name = "Описание")]
        public string Discription { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public EditProviderProfileViewModel()
        {
            
        }

        public EditProviderProfileViewModel(ProviderProfile profile)
        {
            Id = profile.Id;
            CompanyName = profile.CompanyName;
            Discription = profile.Discription;
            Email = profile.ApplicationUser.Email;
            FirstName = profile.FirstName;
            LastName = profile.LastName;
            Patronymic = profile.Patronymic;
            PhoneNumber = profile.ApplicationUser.PhoneNumber;
            ApplicationUser = profile.ApplicationUser;
        }

        public ProviderProfile GetProfile()
        {
            return new ProviderProfile
            {
                Id = Id,
                CompanyName = CompanyName,
                Discription = Discription,
                FirstName = FirstName,
                LastName = LastName,
                Patronymic = Patronymic,
                ApplicationUser = ApplicationUser
            };
        }
    }
}