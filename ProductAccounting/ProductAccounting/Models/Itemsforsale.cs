using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Models
{
    public class Itemsforsale
    {
        public int id { get; set; }
        public int id_sale { get; set; }
        public int id_item { get; set; }

        public Sales IdSaleNavigation{ get; set; }
        public Items IdItemNavigation { get; set; }
    }
}
