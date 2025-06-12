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

        public async Task<SupplierDTO> LoadDataSup(int ID) 
        {
            using (var context = new ApplicationDbContext()) 
            {
                suppliers supplyer = await context.Set<suppliers>().FirstOrDefaultAsync(i => i.id == ID);
                SupplierDTO supplyerDTO = new SupplierDTO
                {
                    Name = supplyer.name,
                    Organform = supplyer.organform,
                    City = supplyer.city,
                    Address = supplyer.address,
                    Rating = supplyer.rating,
                    PhoneNumber = supplyer.phonenumber,
                    Email = supplyer.email,
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

        public async Task<bool> AddSupplier(SupplierUpdateDTO currentDTO) 
        {
            suppliers supplier = new suppliers { name = currentDTO.Name, organform = currentDTO.Organform, city = currentDTO.City, address = currentDTO.Address, rating = currentDTO.Rating,
                phonenumber = currentDTO.PhoneNumber, email = currentDTO.Email
            };
            await DbFunctions.AddData<suppliers>(supplier);
            return true;
        }

        public async Task<bool> ChangeSupplier(int IdSupplier, SupplierUpdateDTO currrentDTO, List<string> changedFields)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                suppliers supplier = context.Set<suppliers>().FirstOrDefault(s => s.id == IdSupplier);
                if (supplier == null) return false;

                if (changedFields.Contains(nameof(SupplierUpdateDTO.Name)))
                    supplier.name = currrentDTO.Name;

                if (changedFields.Contains(nameof(SupplierUpdateDTO.Organform)))
                    supplier.organform = currrentDTO.Organform;

                if (changedFields.Contains(nameof(SupplierUpdateDTO.City)))
                    supplier.city = currrentDTO.City;

                if (changedFields.Contains(nameof(SupplierUpdateDTO.Address)))
                    supplier.address = currrentDTO.Address;

                if (changedFields.Contains(nameof(SupplierUpdateDTO.Rating)))
                    supplier.rating = currrentDTO.Rating;

                if (changedFields.Contains(nameof(SupplierUpdateDTO.PhoneNumber)))
                    supplier.phonenumber = currrentDTO.PhoneNumber;

                if (changedFields.Contains(nameof(SupplierUpdateDTO.Email)))
                    supplier.email = currrentDTO.Email;

                await DbFunctions.ChangeData<suppliers>(supplier);
                return true;
            }

        }
    }
}
