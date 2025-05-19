using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Models
{
    public class Items
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string producttype { get; set; }
        [Required]
        public string color { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public double price { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public double magnitude { get; set; }
        [Required]
        public string unit { get; set; }

        public ICollection<Itemsforsale> itemsforsales { get; set; }
    }
}
