using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marculator.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
