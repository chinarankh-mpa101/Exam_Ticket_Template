using System.ComponentModel.DataAnnotations;

namespace Exam_Ticket_Template.ViewModels.DepartmentViewModels
{
    public class DepartmentCreateVM
    {
        [Required,MaxLength(256)]
        public string Name { get; set; }
    }
}
