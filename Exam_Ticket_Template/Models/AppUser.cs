using Microsoft.AspNetCore.Identity;

namespace Exam_Ticket_Template.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
