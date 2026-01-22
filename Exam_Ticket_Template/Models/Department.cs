using Exam_Ticket_Template.Models.Common;

namespace Exam_Ticket_Template.Models
{
    public class Department:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
