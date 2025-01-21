using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProductAccounting
{
    internal class DbFunctions
    {
        public static void LoadData<T>(DataGrid dataGrid, Expression<Func<T, object>> includeProperty) where T:class
        {
            using (var context = new ApplicationDbContext())
            {
                var data = context.Set<T>().Include(includeProperty).ToList();
                dataGrid.ItemsSource = data;
            }
        }
    }
}
