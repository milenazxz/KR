using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Models
{
    public class Writeoffs
    {
        public int id { get; set; }
        public int id_employee { get; set; }
        public int id_warehouse { get; set; }
        [Required]
        public DateTime date { get; set; }

        public employees IdEmpNavigation { get; set; }
        public Warehouses IdWarehNavigation { get; set; }

        public ICollection<ItemsForWriteOff> writeOffs { get; set; }
    }
}
