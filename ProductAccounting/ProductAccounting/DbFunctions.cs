using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql;
using ProductAccounting.Interfaces;
using ProductAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductAccounting
{
    internal class DbFunctions : IDbFunctions
    {

      /*  protected static Dictionary<string, string> engHeading = new Dictionary<string, string>
        {
            {"ФИО", "name"}
        };*/
        

        /*Функция для удаления элемента из списка и базы данных*/
        public async Task DeleteItem<T>(T itemForDel, Expression<Func<T, bool>> predicate) where T: class
        {
                if (itemForDel == null)
                {
                    return;
                }

                using (var context = new ApplicationDbContext())
                {
                    var itemToRemove = await context.Set<T>().FirstOrDefaultAsync(predicate);
                    if (itemToRemove != null)
                    {
                        context.Set<T>().Remove(itemToRemove);
                        await context.SaveChangesAsync();
                        
                    }
                    else
                    {
                        return;
                    }
                   
                }
        }

        /*Фунция добавления данных в бд*/
        public async Task AddData<T>(T dataObject) where T : class
        {
            if(dataObject == null)
            {
                return;
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

        public async Task ChangeData<T>(T dataObject) where T: class
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                 context.Update<T>(dataObject);
                 await context.SaveChangesAsync();
            }
        }

        /* Функция поиска в базе данных*/
       /* public static async void Search<T>(string colName, string tableName, string value, DataGrid dataGrid) where T : class
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
        }*/
    }
}
