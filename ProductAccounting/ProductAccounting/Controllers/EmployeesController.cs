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
                    Name = employee.name,
                    Post = employee.post,
                    Contacts = employee.contacts,
                };
                return employeeDTO;
            }
        }

        public async Task<bool> AddEmp(EmployeeUpdateDTO currentDTO)
        {
            employees employee = new employees { name = currentDTO.Name, post = currentDTO.Post, contacts = currentDTO.Contacts };
            await DbFunctions.AddData<employees>(employee);
            return true;
        }

        public async Task<bool> ChangeEmployee(int IdEmployee, EmployeeUpdateDTO currentDTO, List<string> changedFields)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                employees employee = context.Set<employees>().FirstOrDefault(e => e.id == IdEmployee);
                if (employee == null) return false;

                if (changedFields.Contains(nameof(EmployeeUpdateDTO.Name)))
                    employee.name = currentDTO.Name;

                if (changedFields.Contains(nameof(EmployeeUpdateDTO.Post)))
                    employee.post = currentDTO.Post;

                if (changedFields.Contains(nameof(EmployeeUpdateDTO.Contacts)))
                    employee.contacts = currentDTO.Contacts;

                await DbFunctions.ChangeData<employees>(employee);
                return true;
            }
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
