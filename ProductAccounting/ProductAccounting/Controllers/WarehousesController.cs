using ProductAccounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProductAccounting.Controllers
{
    public class WarehousesController
    {
        public Warehouses result { get; private set; }

        public async Task<bool> AddWarehouse(string Name, string City, string Address, int Head)
        {
            result = new Warehouses() { name = Name, city = City, address = Address, id_head = Head};
            await DbFunctions.AddData<Warehouses>(result);
            return true;
        }

        public async Task DeleteWarehouses(Warehouses selectItem, Func<Warehouses, bool> lFunc) 
        {
            await DbFunctions.DeleteItem<Warehouses>(selectItem, lFunc);
        }

    }
}
