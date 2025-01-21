using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Models
{
    public class employees
    {
        public int id { get; set; } 
        public string name { get; set; }
        public string post { get; set; }
        public string contacts { get; set; }

        public ICollection<Warehouses> Warehouses { get; set; } 
    }
}
