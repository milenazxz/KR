using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Models
{
    public class Sales
    {
        public int id { get; set; }
        public int? id_employee { get; set; }
        public int? id_client { get; set; }
        public int? id_warehouse { get; set; }

        [Required]
        public string date { get; set; }

        public employees IdEmpNavigation { get; set; }
        public Clients IdClientNavigation { get; set; }
        public Warehouses IdWarehNavigation { get; set; }

        public ICollection<ItemForSale> ItemForSales { get; set; }

    }
}
