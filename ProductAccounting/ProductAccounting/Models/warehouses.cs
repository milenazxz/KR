using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Models
{
    public class Warehouses
    {
        public int id { get; set; }
        
        [Required]
        public string name { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string address { get; set; }
        
        public int? id_head { get; set; }
        
        public employees IdHeadNavigation { get; set; }
    }
}
