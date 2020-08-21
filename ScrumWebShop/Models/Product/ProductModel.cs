using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumWebShop.Models.Product
{
    public class ProductModel
    {
        public int Id { get; set;}
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel CategoryModel { get; set; }
    }
}
