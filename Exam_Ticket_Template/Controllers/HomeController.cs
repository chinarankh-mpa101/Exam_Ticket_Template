using Exam_Ticket_Template.Contexts;
using Exam_Ticket_Template.Models;
using Exam_Ticket_Template.ViewModels.EmployeeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Exam_Ticket_Template.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.Select(x => new EmployeeGetVM
            {
                Id = x.Id,
                ImagePath = x.ImagePath,
                Name = x.Name,
                DepartmentName = x.Department.Name
            }).ToListAsync();
            return View(employees);
           
        }

      
    }
}
