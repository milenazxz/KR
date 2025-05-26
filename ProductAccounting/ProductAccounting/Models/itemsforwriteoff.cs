using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Models
{
    public class ItemsForWriteOff
    {
        public int id { get; set; }
        public int id_writeoff { get; set; }
        public int id_item { get; set; }
        public int quantity { get; set; }

        public Writeoffs IdWriteOffNavigation { get; set; }
        public Items IdItemNavigation { get; set; }
    }
}
