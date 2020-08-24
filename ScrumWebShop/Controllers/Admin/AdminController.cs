using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScrumWebShop.Data;
using ScrumWebShop.Models;

namespace ScrumWebShop.Controllers.Admin
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Roles()
        {
            IEnumerable<IdentityRole> roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult Users()
        {
            var dbusers = _userManager.Users;
            var users = new List<CustomerViewModel>();

            foreach (var user in dbusers)
            {
                var roles = _signInManager.UserManager.GetRolesAsync(user).Result.ToList();

                users.Add(new CustomerViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    StreetAddress = user.StreetAdress,
                    City = user.City,
                    Country = user.Country,
                    PostalCode = user.PostalCode,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    Role = roles[0]
                });
            }

            return View(users);

        }

        [HttpGet]
        public async Task <IActionResult> DeleteUser(string id)
        {
            var userExists = await _userManager.FindByIdAsync(id);

            if (userExists == null)
            {
                ViewBag.ErrorMessage = $"User with Id:  {id} cannot be found";
                    return RedirectToAction("Users");
            }

            var result = await _userManager.DeleteAsync(userExists);

            if (result.Succeeded)
                return RedirectToAction("Users");
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction("Users");
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var userExists = await _userManager.FindByIdAsync(id);

            if (userExists == null)
            {
                ViewBag.ErrorMessage = $"User with Id:  {id} cannot be found";
                return RedirectToAction("Users");
            }

            var model = new CustomerViewModel()
            {
                Id = userExists.Id,
                FirstName = userExists.FirstName,
                LastName = userExists.LastName,
                StreetAddress = userExists.StreetAdress,
                City = userExists.City,
                PostalCode = userExists.PostalCode,
                Country = userExists.Country,
                PhoneNumber = userExists.PhoneNumber,
                Email = userExists.Email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(CustomerViewModel model)
        {
            var userExists = await _userManager.FindByIdAsync(model.Id);

            if (userExists == null)
            {
                ViewBag.ErrorMessage = $"User with Id:  {model.Id} cannot be found";
                return View(model);
            }


            userExists.PhoneNumber = model.PhoneNumber;
            userExists.FirstName = model.FirstName;
            userExists.LastName = model.LastName;
            userExists.StreetAdress = model.StreetAddress;
            userExists.PostalCode = model.PostalCode;
            userExists.City = model.City;
            userExists.Email = model.Email;
            var result = await _userManager.UpdateAsync(userExists);

            if (result.Succeeded)
                return RedirectToAction("Users");
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


    }
}
