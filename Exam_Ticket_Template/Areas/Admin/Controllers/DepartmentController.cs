using Exam_Ticket_Template.Contexts;
using Exam_Ticket_Template.Models;
using Exam_Ticket_Template.ViewModels.DepartmentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Exam_Ticket_Template.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class DepartmentController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments.Select(x=>new DepartmentGetVM
            {
                Id=x.Id,
                Name=x.Name
            }).ToListAsync();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
               
                return View(vm);

            }

            Department departments = new()
            {
                Name = vm.Name
            };

            await _context.Departments.AddAsync(departments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var departments = await _context.Departments.FindAsync(id);
            if(departments is null)
            {
                return NotFound();
            }
            _context.Departments.Remove(departments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var departments = await _context.Departments.FindAsync(id);
            if(departments is null)
            {
                return BadRequest();
            }
            DepartmentUpdateVM vms = new()
            {
                Name = departments.Name
            };
            return View(vms);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DepartmentUpdateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var isExistDepartments = await _context.Departments.FindAsync(vm.Id);
            if(isExistDepartments is null)
            {
                return NotFound();
            }
            isExistDepartments.Name = vm.Name;
            _context.Departments.Update(isExistDepartments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
