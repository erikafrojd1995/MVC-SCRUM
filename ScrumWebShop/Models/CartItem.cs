using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumWebShop.Models
{
    public class CartItem
    {
        //hur jag vill att produkten ska se ut i shoppingcarten
        
        public Guid Id { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
