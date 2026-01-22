using NuGet.Protocol.Core.Types;

namespace Exam_Ticket_Template.ViewModels.EmployeeViewModels
{
    public class EmployeeGetVM
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
    }
}
