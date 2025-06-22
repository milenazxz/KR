using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Models
{
    public class ItemsForSupply
    {
        public int id { get; set; }
        public int id_supply { get; set; }
        public int id_item {  get; set; }
        public int quantity {  get; set; }
        public int nds { get; set; }

        public Supplies IdSupplyNavigation { get; set; }
        public Items IdItemNavigation { get; set; }
    }
}
