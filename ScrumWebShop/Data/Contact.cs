using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumWebShop.Data
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Headline { get; set; }
        [Required]
        public string Message { get; set; }
        
        public DateTime Date { get; set; }
    }
}
