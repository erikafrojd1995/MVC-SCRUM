﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumWebShop.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAdress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set;}
        public string Country { get; set; }

    }
}
