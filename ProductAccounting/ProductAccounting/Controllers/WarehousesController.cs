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
using ProductAccounting.Forms;
using static ProductAccounting.Logger;
using ProductAccounting.Interfaces;


namespace ProductAccounting.Controllers
{
    public class WarehousesController
    {
        public Warehouses result { get; private set; }
        IDbFunctions dbFunctions = new DbFunctions();

        public async Task<List<Warehouses>> LoadData(Expression<Func<Warehouses, object>> lFunc) 
        {
            using (var contex = new ApplicationDbContext())
            {
                return await contex.warehouses.Include(lFunc).ToListAsync();
                
            }
        }

        public async Task<WarehouseDTO> LoadDataWarehouse(int IdItem) 
        {
            using (var context = new ApplicationDbContext())
            {
                var item = await context.Set<Warehouses>().Include(w=> w.IdHeadNavigation)
                    .FirstOrDefaultAsync(w => w.id == IdItem);
                WarehouseDTO warehouseDTO = new WarehouseDTO
                {
                    Name = item.name,
                    City = item.city,
                    Address = item.address,
                    Id_head = item.id_head,
                };
                return warehouseDTO;
            }
            
        }
        
        public async Task<bool> AddWarehouse(WarehouseUpdateDTO warehouseDTO)
        {
            result = new Warehouses() { name = warehouseDTO.Name, city = warehouseDTO.City, address = warehouseDTO.Address, id_head = warehouseDTO.Id_head};
            await dbFunctions.AddData<Warehouses>(result);
            return true;
        }

        public async Task<bool> ChangeWarehouse(int IdWareHouse, WarehouseUpdateDTO warehouseUpdateDTO, List<string> changedFields)
        {

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Warehouses warehouse = context.Set<Warehouses>().FirstOrDefault(w => w.id == IdWareHouse);
                if(warehouse == null) return false;
                if (changedFields.Contains(nameof(WarehouseUpdateDTO.Name)))
                    warehouse.name = warehouseUpdateDTO.Name;

                if (changedFields.Contains(nameof(WarehouseUpdateDTO.City)))
                    warehouse.city = warehouseUpdateDTO.City;

                if (changedFields.Contains(nameof(WarehouseUpdateDTO.Address)))
                    warehouse.address = warehouseUpdateDTO.Address;

                if (changedFields.Contains(nameof(WarehouseUpdateDTO.Id_head)))
                    warehouse.id_head = warehouseUpdateDTO.Id_head;

                await dbFunctions.ChangeData<Warehouses>(warehouse);
                return true;
            }

        }

        public async Task DeleteWarehouses(IList selectItems) 
        {
            foreach (var selectedItem in selectItems)

                if (selectedItem is Warehouses warehouse)
                {
                    await dbFunctions.DeleteItem<Warehouses>(warehouse, w => w.id == warehouse.id);
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

        public async Task<List<Warehouses>> RefreshAsync()
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
