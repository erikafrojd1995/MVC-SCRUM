using ScrumWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumWebShop.Services
{
    public class CartService : ICartService
    {
        private List<CartItem> Cart { get; set; } //vår kundvag, en lista med items.

        public CartService() //konstruktor som aktiverar listan
        {
            Cart = new List<CartItem>();
        }

        public List<CartItem> GetItems() //Hämtar informationen i Carten
        {
            return Cart;
        }

        public void AddItem(Guid id, int quantity = 1)
        {
            var index = Cart.FindIndex(item => item.Id == id); //letar efter en produkt
            if (index != -1) //finns det redan en produkt i korgen
                Cart[index].Quantity += 1; //öka kvantiteten med 1
            else
                Cart.Add(new CartItem { Id = id, Quantity = quantity }); //annars kör bara på och lägg till
        }

        public void RemoveItem(Guid id)
        {
            var index = Cart.FindIndex(item => item.Id == id); //letar efter en produkt
            if (index != -1) //finns det redan en produkt i korgen
                if (Cart[index].Quantity == 1) //ta bort produkten om den går ner till noll
                    Cart.RemoveAt(index);
                else
                    Cart[index].Quantity -= 1;

        }
    }
}
