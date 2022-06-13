using System;
using System.ComponentModel.DataAnnotations;

namespace Marculator.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Display(Name = "Nomi")]
        public string Name { get; set; }

        [Display(Name = "Narxi")]
        public int Price { get; set; }
    }
}
