using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumWebShop.Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public string Brand { get; set; }
        public string Sex { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public string Photo { get; set; }
    }
}
