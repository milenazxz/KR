using Microsoft.EntityFrameworkCore;
using ProductAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Windows.Threading;
using System.Linq.Expressions;


namespace ProductAccounting.Controllers
{
    public class WarehousesController
    {
        public Warehouses result { get; private set; }

        
        public async Task<List<Warehouses>> LoadData( Expression<Func<Warehouses, object>> lFunc) 
        {
            using (var contex = new ApplicationDbContext())
            {
                var loadData = await contex.Set<Warehouses>().Include(lFunc).ToListAsync();
                return loadData;
            }
        }

        public async Task<Warehouses> LoadDataWarehouse(int IdItem) 
        {
            using (var context = new ApplicationDbContext())
            {
                var item = await context.Set<Warehouses>().FirstOrDefaultAsync(w => w.id == IdItem);
                return item;
            }
            
        }
        
        public async Task<bool> AddWarehouse(string Name, string City, string Address, int Head)
        {
            result = new Warehouses() { name = Name, city = City, address = Address, id_head = Head};
            await DbFunctions.AddData<Warehouses>(result);
            return true;
        }

        public async Task DeleteWarehouses(IList selectItems) 
        {
            foreach (var selectedItem in selectItems)

                if (selectedItem is Warehouses warehouse)
                {
                    await DbFunctions.DeleteItem<Warehouses>(warehouse, w => w.id == warehouse.id);
                }
            
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> LoadForCombobox()
        {
            using (var context = new ApplicationDbContext())
            {
                var result = await context.employees
                  .Select(e => new KeyValuePair<int, string>(e.id, e.name))
                  .ToListAsync();
                return result;
            }
            
        }

        public async Task<List<Warehouses>> RefreshAsync(DataGrid dataGrid)
        {
            using (var context = new ApplicationDbContext()) 
            {
                List<Warehouses> updatedData = new List<Warehouses>();

                updatedData = await context.Set<Warehouses>().Include(w => w.IdHeadNavigation).ToListAsync() as List<Warehouses>;
                return updatedData;
            }
        }

    }
}
