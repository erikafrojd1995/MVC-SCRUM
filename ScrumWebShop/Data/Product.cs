using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumWebShop.Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Produktnummer")]
        public string ProductNumber { get; set; }

        [Required]
        [DisplayName("Produktnamn")]
        public string ProductName { get; set; }

        [Required]
        [DisplayName("Pris")]
        public decimal ProductPrice { get; set; }

        [Required]
        [DisplayName("Beskrivning")]
        public string ProductDescription { get; set; }

        [Required]
        [DisplayName("Brand")]
        public string Brand { get; set; }

        [Required]
        [DisplayName("Kön")]
        public string Sex { get; set; }

        [Required]
        [DisplayName("Färg")]
        public string Color { get; set; }

        //[Required]
        [DisplayName("Bild")]
        public string Photo { get; set; }
    }
}

