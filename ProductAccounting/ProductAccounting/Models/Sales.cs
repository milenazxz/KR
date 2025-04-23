using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Models
{
    internal class Sales
    {
        public int id { get; set; }
        public int? id_employee { get; set; }
        public int? id_client { get; set; }
        public int? id_warehouses { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public employees IdEmpNavigation { get; set; }
        public Clients IdClientNavigation { get; set; }
        public Warehouses IdWarehNavigation { get; set; }

    }
}
