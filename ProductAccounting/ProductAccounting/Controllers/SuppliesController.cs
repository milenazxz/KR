using Microsoft.EntityFrameworkCore;
using ProductAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Controllers
{
    public class SuppliesController
    {
        public async Task<List<Supplies>> LoadData(Expression<Func<Supplies, object>> WFunc, Expression<Func<Supplies, object>> CFunc, Expression<Func<Supplies, object>> EFunc)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.supplies
                    .Include(WFunc)
                    .Include(CFunc)
                    .Include(EFunc)
                    .ToListAsync();
            }
        }

        public async Task DeleteItems(IList selectedItems)
        {
            foreach (var item in selectedItems)
            {
                if (item is Supplies supply)
                {
                    await DbFunctions.DeleteItem<Supplies>(supply, s => s.id == supply.id);
                }
            }
        }
    }
}
