using System.ComponentModel.DataAnnotations;

namespace Exam_Ticket_Template.ViewModels.UserViewModels
{
    public class RegisterVM
    {
        [Required,MaxLength(1024)]
        public string Fullname { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required,MaxLength(1024)]
        public string Username { get; set; }
        [Required,MinLength(6), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MinLength(6), DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
