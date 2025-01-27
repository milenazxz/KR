using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProductAccounting
{
    internal class DbFunctions
    {
        /*Функция для загрузки данных из базы данных*/
        public static void LoadData<T>(DataGrid dataGrid, Expression<Func<T, object>> includeProperty) where T:class
        {
            using (var context = new ApplicationDbContext())
            {
                var data = context.Set<T>().Include(includeProperty).ToList();
                dataGrid.ItemsSource = data;
            }
        }
        public static void LoadData<T>(DataGrid dataGrid) where T : class
        {
            using (var context = new ApplicationDbContext())
            {
                var data = context.Set<T>().ToList();
                dataGrid.ItemsSource = data;
            }
        }

        /*Функция для удаления элемента из списка и базы данных*/
        public static async Task DeleteItem<T>(T itemForDel, DataGrid dataGrid, Func<T,bool> lFunc) where T: class
        {
                if (itemForDel == null)
                {
                    MessageBox.Show("Объект для удаления не выбран");
                    return;
                }

                using (var context = new ApplicationDbContext())
                {
                    var itemToRemove = await Task.Run(() => context.Set<T>().FirstOrDefault(lFunc));
                    if (itemToRemove != null)
                    {
                        context.Set<T>().Remove(itemToRemove);
                        await context.SaveChangesAsync();
                        
                        var updatedData = await context.Set<T>().ToListAsync();
                        
                        dataGrid.Dispatcher.Invoke(() =>
                        {
                            dataGrid.ItemsSource = updatedData;
                        });
                        
                    }
                    else
                    {
                        MessageBox.Show("Элемент с данным ID не найден");
                    }
                   
                }
        }
    }
}
