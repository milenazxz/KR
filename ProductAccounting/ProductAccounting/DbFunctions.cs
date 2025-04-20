using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ProductAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductAccounting
{
    internal class DbFunctions
    {

        protected static Dictionary<string, string> engHeading = new Dictionary<string, string>
        {
            {"ФИО", "name"}
        };
        
        
        /*Функция для загрузки данных из базы данных*/
        public static async Task LoadDataAsync<T>(DataGrid dataGrid, Expression<Func<T, object>> includeProperty) where T:class
        {
            using (var context = new ApplicationDbContext())
            {
                var data = await context.Set<T>().Include(includeProperty).ToListAsync();
                await dataGrid.Dispatcher.InvokeAsync(() =>
                {
                    dataGrid.ItemsSource = data;
                });
            }
        }
        public static async Task LoadDataAsync<T>(DataGrid dataGrid) where T : class
        {
            using (var context = new ApplicationDbContext())
            {
                var data = await context.Set<T>().ToListAsync();
                await dataGrid.Dispatcher.InvokeAsync(() =>
                {
                    dataGrid.ItemsSource = data;
                });
            }
        }

        /*Функция для удаления элемента из списка и базы данных*/
        public static async Task DeleteItem<T>(T itemForDel, Func<T,bool> lFunc) where T: class
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
                        
                    }
                    else
                    {
                        MessageBox.Show("Элемент с данным ID не найден");
                    }
                   
                }
        }

        /*Фунция добавления данных в бд*/
        public static async Task AddData<T>(T dataObject) where T : class
        {
            if(dataObject == null)
            {
                MessageBox.Show("Не удалось добавить данные");
            }
            else
            {
                using (var context = new ApplicationDbContext())
                {
                    await context.Set<T>().AddAsync(dataObject);
                    await context.SaveChangesAsync();
                
                }
            }
            
            
        }

        /*Функция обновления данных*/
        public static async Task RefreshAsync<T>(DataGrid dataGrid) where T : class
        {
            using (var context = new ApplicationDbContext())
            {
                var updatedData = await context.Set<T>().ToListAsync();
                dataGrid.Dispatcher.Invoke(() =>
                {
                    dataGrid.ItemsSource = updatedData;
                });
            }
        }
        public static void Refresh<T>(DataGrid dataGrid, IEnumerable inpValues) where T : class
        {
          
            dataGrid.Dispatcher.InvokeAsync(() =>
                {
                    dataGrid.ItemsSource = inpValues;
                });
            
        }

        /* Функция поиска в базе данных*/
        public static async void Search<T>(string colName, string tableName, string value, DataGrid dataGrid) where T : class
        {
            
            var connectionString = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;

            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                string DbColumnName = "";
                if (DbFunctions.engHeading.TryGetValue(tableName, out string dbColumnName))
                {
                    DbColumnName = dbColumnName;
                }
                var query = "SELECT * FROM " + DbColumnName + " WHERE " + colName + " LIKE @searchValue";
                var result = await connection.QueryAsync<T>(query, new { searchValue = $"%{value}%" });
                if (result == null)
                {
                    InaccurateSearch<T>(colName, DbColumnName);
                }
                else
                {
                   // RefreshAsync<T>(dataGrid, result);
                }
                
            }
              
        }
        public static async void InaccurateSearch<T>(string colName, string tableName) where T : class
        {
            var connectionString = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;

            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var query = "SELECT * FROM " + tableName + " WHERE " + colName;
                var result = await connection.QueryAsync<T>(query);
            }
        }

        public static List<string> GetAllHeaders(DataGrid dataGrid)
        {
            List<string> headers = dataGrid.Columns.OfType<DataGridTextColumn>().Select(col => col.Header.ToString()).ToList();
            return headers;
        }
    }
}
