using Microsoft.EntityFrameworkCore;
using ProductAccounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Controllers
{
    internal class SalesController
    {
        public async Task<List<Sales>> LoadData(Expression<Func<Sales, object>> WFunc, Expression<Func<Sales, object>> CFunc, Expression<Func<Sales, object>> EFunc)
        {
            using (var context = new ApplicationDbContext()) 
            {
                return await context.sales
                    .Include(WFunc)
                    .Include(CFunc)
                    .Include(EFunc)
                    .ToListAsync();
            }
        }
    }
}
