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
    internal class EmployeesController
    {
        public async Task<List<employees>> LoadData() 
        {
            using (var contex = new ApplicationDbContext()) 
            {
                List<employees> loadedData = await contex.Set<employees>().ToListAsync();
                return loadedData;
            }
        }

        public async Task<employees> LoadDataEmp(int ID)
        {
            using (var context = new ApplicationDbContext())
            {
                var item = await context.Set<employees>().FirstOrDefaultAsync(i => i.id == ID);
                return item;
            }
        }

        public async Task<bool> AddEmp(string Name, string Post, string Contacts)
        {
            employees employee = new employees { name = Name, post = Post, contacts = Contacts};
            await DbFunctions.AddData<employees>(employee);
            return true;
        }

        public async Task DeleteEmployee(IList selectedCEmployees)
        {
            foreach (var selectedEmployee in selectedCEmployees)
            {
                if (selectedEmployee is employees employee)
                {
                    await DbFunctions.DeleteItem<employees>(employee, е => е.id == employee.id);
                }
            }

        }
    }
}
