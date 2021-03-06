﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScrumWebShop.Models;


namespace ScrumWebShop.Data
{
    public class ApplicationDbContext : IdentityDbContext <ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ScrumWebShop.Models.CustomerViewModel> CustomerViewModel { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ScrumWebShop.Models.Order> Order { get; set; }
        public DbSet<CartItem> OrderItems { get; set; }
    }
}
