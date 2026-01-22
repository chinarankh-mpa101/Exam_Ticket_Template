using System.ComponentModel.DataAnnotations;

namespace Exam_Ticket_Template.ViewModels.EmployeeViewModels
{
    public class EmployeeCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required,MaxLength(256)]
        public string Name { get; set; }
        [Required]
        public int DepartmentId { get; set; }
    }
}
