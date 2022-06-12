using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
