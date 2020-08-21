using ScrumWebShop.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumWebShop.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        public string Brand { get; set;}
        public string Sex { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }

        public ICollection<ProductModel> ProductModels { get; set; }
    }
}
