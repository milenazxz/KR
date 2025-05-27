using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Models
{


    public class employees
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        [RegularExpression("Директор|Работник склада|Экономист-аналитик",
        ErrorMessage = "Недопустимая должность")]
        public string post { get; set; }
        
        [Required]
        public string contacts { get; set; }

        public string login { get; set; }

        public string emp_password { get; set; } 

        public ICollection<Warehouses> Warehouses { get; set; } 
        public ICollection <Sales> Sales { get; set; }
        public ICollection<Writeoffs> Writeoffs { get; set; }
        public ICollection<Supplies> Supplies { get; set; }

    }
}
