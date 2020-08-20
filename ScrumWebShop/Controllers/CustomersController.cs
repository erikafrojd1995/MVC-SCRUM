using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScrumWebShop.Data;
using ScrumWebShop.Models;

namespace ScrumWebShop.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class CustomersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CustomersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult AllCustomers()
        {
            var dbCustomers = _userManager.Users;
            var customers = new List<CustomerViewModel>();

            foreach(var customer in dbCustomers)
            {
                var roles = _signInManager.UserManager.GetRolesAsync(customer).Result.ToList();

                customers.Add(new CustomerViewModel
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    StreetAddress = customer.StreetAddress,
                    City = customer.City,
                    Zipcode = customer.Zipcode,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    UserName = customer.UserName,
                    Role = roles[0]
                });
            }
            return View(customers);
        }
    }
}
