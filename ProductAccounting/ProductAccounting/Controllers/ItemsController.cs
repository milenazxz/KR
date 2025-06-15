using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProductAccounting.Forms;
using ProductAccounting.Interfaces;
using ProductAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ProductAccounting.Controllers
{
    internal class ItemsController
    {
        IDbFunctions dbFunctions = new DbFunctions();
        public async Task<List<Items>> LoadData() 
        {
            using (var context = new ApplicationDbContext())
            {
                var items = await context.Set<Items>().ToListAsync();
                return items;
            }
        }
        public async Task<ItemDTO> LoadDataItem(int IdItem)
        {
            using (var context = new ApplicationDbContext())
            {
                Items item = await context.Set<Items>().FirstOrDefaultAsync(i => i.id == IdItem);
                ItemDTO itemDTO = new ItemDTO
                {
                    Name = item.name,
                    ProducTtype = item.producttype,
                    Color = item.color,
                    Price = item.price,
                    Magnitude = item.magnitude,
                    Unit = item.unit,
                };
                return itemDTO;
            }

        }

        public async Task<bool> AddItem(ItemUpdateDTO currentDTO) 
        {
            var addItem = new Items {name = currentDTO.Name, producttype = currentDTO.ProducTtype, color = currentDTO.Color, price = currentDTO.Price, magnitude = currentDTO.Magnitude, unit = currentDTO.Unit };
            await dbFunctions.AddData<Items>(addItem);
            return true;
        }

        public async Task<bool> ChangeItem(int IdItem, ItemUpdateDTO currentDTO, List<string> changedFields)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Items item = context.Set<Items>().FirstOrDefault(i => i.id == IdItem);
                if (item == null) return false;

                if (changedFields.Contains(nameof(currentDTO.Name)))
                    item.name = currentDTO.Name;

                if (changedFields.Contains(nameof(currentDTO.ProducTtype)))
                    item.producttype = currentDTO.ProducTtype;

                if (changedFields.Contains(nameof(currentDTO.Color)))
                    item.color = currentDTO.Color;

                if (changedFields.Contains(nameof(currentDTO.Price)))
                    item.price = currentDTO.Price;

                if (changedFields.Contains(nameof(currentDTO.Magnitude)))
                    item.magnitude = currentDTO.Magnitude;

                if (changedFields.Contains(nameof(currentDTO.Unit)))
                    item.unit = currentDTO.Unit;

                await dbFunctions.ChangeData<Items>(item);
                return true;
            }
        }


        public async Task DeleteItem(IList selectedItems) 
        {
            foreach (var selecteItem in selectedItems) 
            {
                if (selecteItem is Items item) 
                {
                    await dbFunctions.DeleteItem<Items>(item, i => i.id == item.id);
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
