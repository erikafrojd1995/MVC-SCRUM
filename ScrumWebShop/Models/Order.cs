using ScrumWebShop.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumWebShop.Models
{
    public class Order
    {
        
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual ICollection<CartItem> Items { get; set; }
        

    }
}
