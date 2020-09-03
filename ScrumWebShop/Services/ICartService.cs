using ScrumWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumWebShop.Services
{
    public interface ICartService
    {

        List<CartItem> GetItems(); //hämtar items
        void AddItem(Guid id, int quantity = 1); //lägget till items
        void RemoveItem(Guid id); //tar bort items
    }
}
