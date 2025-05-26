using Microsoft.EntityFrameworkCore;
using ProductAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductAccounting.Controllers
{
    public class SuppliesController
    {
        public async Task<List<Supplies>> LoadData(Expression<Func<Supplies, object>> WFunc, Expression<Func<Supplies, object>> CFunc, Expression<Func<Supplies, object>> EFunc)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.supplies
                    .Include(WFunc)
                    .Include(CFunc)
                    .Include(EFunc)
                    .ToListAsync();
            }
        }

        public async Task DeleteItems(IList selectedItems)
        {
            foreach (var item in selectedItems)
            {
                if (item is Supplies supply)
                {
                    await DbFunctions.DeleteItem<Supplies>(supply, s => s.id == supply.id);
                }
            }
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> LoadForComboBoxEmp() 
        {
            using (ApplicationDbContext context = new ApplicationDbContext()) 
            {
                var result = await context.employees
                    .Select(e => new KeyValuePair<int, string>(e.id, e.name))
                    .ToListAsync();
                return result;
            }
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> LoadForComboBoxSup() 
        {
            using (ApplicationDbContext context = new ApplicationDbContext()) 
            {
                var result = await context.suppliers
                    .Select(s => new KeyValuePair<int, string>(s.id, s.name))
                    .ToListAsync();
                return result;
            }
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> LoadForComboBoxWareh()
        {
            using (ApplicationDbContext context = new ApplicationDbContext()) 
            {
                var result = await context.warehouses
                    .Select(w => new KeyValuePair<int, string>(w.id, w.name))
                    .ToListAsync ();
                return result;
            }
        }

        public async Task<int> AddSupply(int inpId_head, int inpId_warehouse, int inpId_supplier, DateTime date)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Supplies supply = new Supplies { id_supplier = inpId_supplier, id_employee = inpId_head, id_warehouse = inpId_warehouse, date = date };
                await context.AddAsync<Supplies>(supply);
                await context.SaveChangesAsync();
                return supply.id;
            }
        }

        public async Task AddItemsForSupply(int inpSupply_id, List<int> items_id, List<int> items_quantity)
        {
            List<ItemsForSupply> items = new List<ItemsForSupply>();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                for (int i = 0; i < items_id.Count(); i++)
                {
                    items.Add(new ItemsForSupply { id_supply = inpSupply_id, id_item = items_id[i], quantity = items_quantity[i] });
                }
                await context.AddRangeAsync(items);
                await context.SaveChangesAsync();
            }
        }

    }
}
