using Microsoft.EntityFrameworkCore;
using ProductAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Controllers
{
    internal class ItemsController
    {
        public async Task<List<Items>> LoadData() 
        {
            using (var context = new ApplicationDbContext())
            {
                var items = await context.Set<Items>().ToListAsync();
                return items;
            }
        }
        public async Task<Items> LoadDataItem(int IdItem)
        {
            using (var context = new ApplicationDbContext())
            {
                var item = await context.Set<Items>().FirstOrDefaultAsync(i => i.id == IdItem);
                return item;
            }

        }

        public async Task<bool> AddItem(string Name, string Producttype, string Color, double Price, double Magnitude, string Unit) 
        {
            var addItem = new Items {name = Name, producttype = Producttype, color = Color, price = Price, magnitude = Magnitude, unit = Unit};
            await DbFunctions.AddData<Items>(addItem);
            return true;
        }
        public async Task DeleteItem(IList selectedItems) 
        {
            foreach (var selecteItem in selectedItems) 
            {
                if (selecteItem is Items item) 
                {
                    await DbFunctions.DeleteItem<Items>(item, i => i.id == item.id);
                }
               
            }
        }

        public async Task<List<Items>> RefreshAsync() 
        {
            using (var context = new ApplicationDbContext()) 
            {
                List<Items> updatedData = await context.Set<Items>().ToListAsync(); 
                return updatedData;
            }
        }
    }
}
