using Exam_Ticket_Template.Models.Common;

namespace Exam_Ticket_Template.Models
{
    public class Employee:BaseEntity
    {
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
