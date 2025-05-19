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
    public class WriteOffsController
    {
        public async Task<List<Writeoffs>> LoadData(Expression<Func<Writeoffs, object>> WFunc, Expression<Func<Writeoffs, object>> EFunc)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.writeoffs
                    .Include(WFunc)
                    .Include(EFunc)
                    .ToListAsync();
            }
        }

        public async Task DeleteItems(IList selectedItems)
        {
            foreach (var item in selectedItems)
            {
                if (item is Writeoffs writeoffs)
                {
                    await DbFunctions.DeleteItem<Writeoffs>(writeoffs, s => s.id == writeoffs.id);
                }
            }
        }
    }
}
