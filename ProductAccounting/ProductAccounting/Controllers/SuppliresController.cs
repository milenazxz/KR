using Microsoft.EntityFrameworkCore;
using ProductAccounting.Forms;
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

        public async Task<SupplyerDTO> LoadDataSup(int ID) 
        {
            using (var context = new ApplicationDbContext()) 
            {
                suppliers supplyer = await context.Set<suppliers>().FirstOrDefaultAsync(i => i.id == ID);
                SupplyerDTO supplyerDTO = new SupplyerDTO
                {
                    name = supplyer.name,
                    organform = supplyer.organform,
                    city = supplyer.city,
                    address = supplyer.address,
                    rating = supplyer.rating,
                    phonenumber = supplyer.phonenumber,
                    email = supplyer.email,
                };
                return supplyerDTO;
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

        public async Task<bool> ChangeSupplier(int IdSupplier, string name, string organform, string city, string address, int rating, string phone, string email)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                suppliers supplyer = context.Set<suppliers>().FirstOrDefault(s => s.id == IdSupplier);
                if (supplyer != null)
                {
                    supplyer.name = name;
                    supplyer.organform = organform;
                    supplyer.city = city;
                    supplyer.address = address;
                    supplyer.rating = rating;
                    supplyer.phonenumber = phone;
                    supplyer.email = email;
                    await DbFunctions.ChangeData<suppliers>(supplyer);
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
