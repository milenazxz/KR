using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Models
{
    public class Clients
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }

        [Required]
        [RegularExpression ("Юридическое лицо|Физическое лицо|Индивидуальный предприниматель", ErrorMessage = "Недопустимая должность")]
        public string organform { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string phonenumber { get; set; }
        public string email { get; set; }

        public ICollection<Sales> Sales { get; set; }
    }
}
