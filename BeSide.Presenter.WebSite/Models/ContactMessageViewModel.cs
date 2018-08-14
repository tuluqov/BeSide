using System.ComponentModel.DataAnnotations;
using BeSide.Common.Entities;

namespace BeSide.Presenter.WebSite.Models
{
    public class ContactMessageViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }

        public ContactMessageViewModel(ContactMessage message)
        {
            Id = message.Id;
            Email = message.Email;
            Name = message.Name;
            Text = message.Text;
            PhoneNumber = message.PhoneNumber;
        }

        public ContactMessage GetMessage()
        {
            return new ContactMessage
            {
                Id = Id, 
                Name = Name,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Text = Text
            };
        }
    }
}