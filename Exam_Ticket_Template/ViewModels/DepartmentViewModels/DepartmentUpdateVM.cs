using System.ComponentModel.DataAnnotations;

namespace Exam_Ticket_Template.ViewModels.DepartmentViewModels
{
    public class DepartmentUpdateVM
    {
        [Required]
        public int Id { get; set; }

        [Required,MaxLength(256)]
        public string Name { get; set; }
    }
}
