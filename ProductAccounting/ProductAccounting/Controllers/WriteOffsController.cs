using Microsoft.EntityFrameworkCore;
using ProductAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Controllers
{
    public class WriteOffsController
    {
        public async Task<List<Writeoffs>> LoadData(Expression<Func<Writeoffs, object>> WFunc, Expression<Func<Writeoffs, object>> EFunc)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.writeoffs
                    .Include(WFunc)
                    .Include(EFunc)
                    .ToListAsync();
            }
        }

        public async Task DeleteItems(IList selectedItems)
        {
            foreach (var item in selectedItems)
            {
                if (item is Writeoffs writeoffs)
                {
                    await DbFunctions.DeleteItem<Writeoffs>(writeoffs, s => s.id == writeoffs.id);
                }
            }
        }


        public async Task<IEnumerable<KeyValuePair<int, string>>> LoadComboBoxEmp() 
        {
            using (ApplicationDbContext context = new ApplicationDbContext()) 
            {
                var result = await context.employees
                    .Select(e => new KeyValuePair<int, string>(e.id, e.name))
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<KeyValuePair<int, string>>> LoadComboBoxWare()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var result = await context.warehouses
                    .Select(e => new KeyValuePair<int, string>(e.id, e.name))
                    .ToListAsync();
                return result;
            }
        }
        public async Task<int> AddWriteOff(int inpId_head, int inpId_warehouse, DateTime date)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Writeoffs writeOff = new Writeoffs{ id_employee = inpId_head, id_warehouse = inpId_warehouse, date = date };
                await context.AddAsync<Writeoffs>(writeOff);
                await context.SaveChangesAsync();
                return writeOff.id;
            }
        }

        public async Task AddItemsForWriteOff(int inpWriteOff_id, List<int> items_id, List<int> items_quantity)
        {
            List<ItemsForWriteOff> items = new List<ItemsForWriteOff>();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                for (int i = 0; i < items_id.Count(); i++) 
                {
                    items.Add(new ItemsForWriteOff { id_writeoff = inpWriteOff_id, id_item = items_id[i], quantity = items_quantity[i] });
                }
                await context.AddRangeAsync(items);
                await context.SaveChangesAsync();
            }
        }

    }
}
