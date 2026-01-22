using Exam_Ticket_Template.Contexts;
using Exam_Ticket_Template.Helpers;
using Exam_Ticket_Template.Models;
using Exam_Ticket_Template.ViewModels.EmployeeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Exam_Ticket_Template.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class EmployeeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AppDbContext _context;
        private readonly string _folderPath;

        public EmployeeController(IWebHostEnvironment environment, AppDbContext context)
        {
            _environment = environment;
            _context = context;
            _folderPath = Path.Combine(_environment.WebRootPath,"assets","images");
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.Select(x=>new EmployeeGetVM
            {
                Id=x.Id,
                ImagePath=x.ImagePath,
                Name=x.Name,
                DepartmentName=x.Department.Name
            }).ToListAsync();
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            await SendDepartmentsWithViewBag();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateVM vm)
        {
            await SendDepartmentsWithViewBag();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var departments = await _context.Departments.AnyAsync(x =>x.Id==vm.DepartmentId);
            if(!departments)
            {
                ModelState.AddModelError("DepartmentId", "Bele bir department id movcud deyill ");
                return View(vm);
            }
            if (vm.Image.Length > 2 * 1024 * 1204)
            {
                ModelState.AddModelError("Image", "Image size must be 2mb");
                return View(vm);
            }
            if (!vm.Image.ContentType.ToLower().Contains("image"))
            {
                ModelState.AddModelError("Image", "Image must be image format");
                return View(vm);
            }

            string uniqueFileName = await vm.Image.FileUploadAsync(_folderPath);
            Employee employee = new()
            {
                ImagePath = uniqueFileName,
                Name = vm.Name,
                DepartmentId = vm.DepartmentId
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int id)
        {
            var employees = await _context.Employees.FindAsync(id);
            if(employees is null)
            {
                return NotFound();
            }
           
            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();
            string oldImagePath = Path.Combine(_folderPath, employees.ImagePath);
            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);

            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Update(int id)
        {
            var employees = await _context.Employees.FindAsync(id);
            if(employees is null)
            {
                return NotFound();
            }
            EmployeeUpdateVM vm = new()
            {

                Name = employees.Name,
                DepartmentId = employees.DepartmentId

            };
            await SendDepartmentsWithViewBag();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeUpdateVM vm)
        {
            await SendDepartmentsWithViewBag();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var departments = await _context.Departments.AnyAsync(x => x.Id == vm.DepartmentId);
            if (!departments)
            {
                ModelState.AddModelError("DepartmentId", "Bele bir department id movcud deyill ");
                return View(vm);
            }
            if (vm.Image.Length > 2 * 1024 * 1204)
            {
                ModelState.AddModelError("Image", "Image size must be 2mb");
                return View(vm);
            }
            if (!vm.Image.ContentType.ToLower().Contains("image"))
            {
                ModelState.AddModelError("Image", "Image must be image format");
                return View(vm);
            }

            var isExistEmployees = await _context.Employees.FindAsync(vm.Id);
            if(isExistEmployees is null)
            {
                return NotFound();
            }
            isExistEmployees.Name = vm.Name;
            isExistEmployees.DepartmentId = vm.DepartmentId;

            if(vm.Image is { })
            {
                string newImagePath = await vm.Image.FileUploadAsync(_folderPath);
                string oldImagePath = Path.Combine(_folderPath, isExistEmployees.ImagePath);
                if (System.IO.File.Exists(oldImagePath))
                    System.IO.File.Delete(oldImagePath);
                isExistEmployees.ImagePath = newImagePath;
            }

             _context.Employees.Update(isExistEmployees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }


        private async Task SendDepartmentsWithViewBag()
        {
            var departments = await _context.Departments.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToListAsync();
            ViewBag.Departments = departments;
        }


    }
}
