using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Models
{
    public class suppliers
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        [Range(1,10)]
        public int rating { get; set; }
        [Required]
        public string phonenumber { get; set; }
        [Required]
        public string email { get; set; }
    }
}
