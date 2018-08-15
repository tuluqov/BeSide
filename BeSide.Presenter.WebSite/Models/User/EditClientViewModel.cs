using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BeSide.Common.Entities;

namespace BeSide.Presenter.WebSite.Models.User
{
    public class EditClientViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
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

        public ApplicationUser ApplicationUser { get; set; }

        public EditClientViewModel()
        {
            
        }

        public EditClientViewModel(ClientProfile profile)
        {
            Id = profile.Id;
            Email = profile.ApplicationUser.Email;
            FirstName = profile.FirstName;
            LastName = profile.LastName;
            Patronymic = profile.Patronymic;
            PhoneNumber = profile.ApplicationUser.PhoneNumber;
            ApplicationUser = profile.ApplicationUser;
        }

        public ClientProfile GetProfile()
        {
            return new ClientProfile
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Patronymic = Patronymic,
            };
        }
    }
}