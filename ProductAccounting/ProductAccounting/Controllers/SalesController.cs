using Microsoft.EntityFrameworkCore;
using ProductAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Controllers
{
    public class SalesController
    {

        public async Task<List<Sales>> LoadData(Expression<Func<Sales, object>> WFunc, Expression<Func<Sales, object>> CFunc, Expression<Func<Sales, object>> EFunc)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.sales
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
                if (item is Sales sale)
                {
                    await DbFunctions.DeleteItem<Sales>(sale, i => i.id == sale.id);
                }
            }
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> LoadForComboboxEmp()
        {
            using (var context = new ApplicationDbContext())
            {
                var result = await context.employees
                  .Select(e => new KeyValuePair<int, string>(e.id, e.name))
                  .ToListAsync();
                return result;
            }

        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> LoadForComboboxWareh()
        {
            using (var context = new ApplicationDbContext())
            {
                var result = await context.warehouses
                  .Select(w => new KeyValuePair<int, string>(w.id, w.name))
                  .ToListAsync();
                return result;
            }

        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> LoadForComboboxClient()
        {
            using (var context = new ApplicationDbContext())
            {
                var result = await context.clients
                  .Select(c => new KeyValuePair<int, string>(c.id, c.name))
                  .ToListAsync();
                return result;
            }

        }

        public async Task<List<Items>> LoadItemsAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.items.ToListAsync();
            }
        }

        public async Task<int> AddSale(int inpId_head, int inpId_warehouse, int inpId_client, DateTime date) 
        {
            using (ApplicationDbContext context = new ApplicationDbContext()) 
            {
                Sales sale = new Sales {id_client = inpId_client, id_employee = inpId_head, id_warehouse = inpId_warehouse, date = date};
                await context.AddAsync<Sales>(sale);
                await context.SaveChangesAsync();
                return sale.id;
            }
        }

        public async Task AddItemsForSale(int inpSale_id, List<int> items_id, List<int> items_quantity) 
        {
            List<ItemForSale> items = new List<ItemForSale>();
            using (ApplicationDbContext context = new ApplicationDbContext()) 
            {
                for (int i = 0; i < items_id.Count(); i++) 
                {
                    items.Add(new ItemForSale { id_sale = inpSale_id, id_item = items_id[i], quantity = items_quantity[i] });
                }
                await context.AddRangeAsync(items);
                await context.SaveChangesAsync();
            }
        }
    }
}
