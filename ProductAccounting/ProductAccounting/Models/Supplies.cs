using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Models
{
    public class Supplies
    {
        public int id { get; set; }
        public int id_employee { get; set; }
        public int id_supplier { get; set; }
        public int id_warehouse { get; set; }
        public DateTime date { get; set; }

        public employees IdEmpNavigation { get; set; }
        public suppliers IdSupNavigation { get; set; }
        public Warehouses IdWarehNavigation { get; set; }

        public ICollection<ItemsForSupply> itemsforsupplies { get; set; }
    }
}
