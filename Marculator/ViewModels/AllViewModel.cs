using Marculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marculator.ViewModels
{
    public class AllViewModel
    {
        public IEnumerable<Product> AllProduct { get; set; }
        public string SearchText { get; set; }
    }
}
