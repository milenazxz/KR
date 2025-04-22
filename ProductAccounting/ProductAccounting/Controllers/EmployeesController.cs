using Microsoft.EntityFrameworkCore;
using ProductAccounting.Models;
using System;
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
    }
}
