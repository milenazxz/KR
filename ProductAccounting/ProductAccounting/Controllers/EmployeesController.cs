using Microsoft.EntityFrameworkCore;
using ProductAccounting.Forms;
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

        public async Task<EmployeeDTO> LoadDataEmp(int ID)
        {
            using (var context = new ApplicationDbContext())
            {
                employees employee= await context.Set<employees>().FirstOrDefaultAsync(i => i.id == ID);
                EmployeeDTO employeeDTO = new EmployeeDTO
                {
                    name = employee.name,
                    post = employee.post,
                    contacts = employee.contacts,
                };
                return employeeDTO;
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
