using Exam_Ticket_Template.Models;
using Exam_Ticket_Template.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Exam_Ticket_Template.Controllers
{
    public class AccountController(SignInManager<AppUser>_signInManager, UserManager<AppUser>_userManager, RoleManager<IdentityRole>_roleManager) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser user = new()
            {
                Fullname = vm.Fullname,
                Email=vm.Email,
                UserName=vm.Username
            };

            var result = await _userManager.CreateAsync(user,vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    
                }
                return View(vm);
            }
            await _userManager.AddToRoleAsync(user, "Member");
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = await _userManager.FindByEmailAsync(vm.Email);
            if(user is null)
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View(vm);
            }
            await _signInManager.PasswordSignInAsync(user, vm.Password, false, true);
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Admin"
            });

            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Member"
            });

            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Moderator"
            });
            return Ok("ROLES CREATED SUCCESSFULLY!");
        }
    }
}
