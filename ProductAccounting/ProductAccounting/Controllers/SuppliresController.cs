using Microsoft.EntityFrameworkCore;
using ProductAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace ProductAccounting.Controllers
{
    internal class SuppliresController
    {
        public async Task<List<suppliers>> LoadData() 
        {
            using (var context = new ApplicationDbContext()) 
            {
                var loadedData = await context.Set<suppliers>().ToListAsync();
                return loadedData;
            }
        }

        public async Task<suppliers> LoadDataSup(int ID) 
        {
            using (var context = new ApplicationDbContext()) 
            {
                var loadedItem = await context.Set<suppliers>().FirstOrDefaultAsync(i => i.id == ID);
                return loadedItem;
            }
        }
        public async Task DeleteSupplier(IList selectedItems) 
        {
            foreach (var selectdItem in selectedItems) 
            {
                if (selectdItem is suppliers item) 
                {
                    await DbFunctions.DeleteItem(item, i => i.id == item.id);
                }
            }
        }

        public async Task<bool> AddSupplier(string Name, string Organform, string City, string Address, int Rating, string Phone, string Email) 
        {
            suppliers supplier = new suppliers { name = Name, organform = Organform, city = City, address = Address, rating = Rating, phonenumber = Phone, email = Email};
            await DbFunctions.AddData<suppliers>(supplier);
            return true;
        }
    }
}
