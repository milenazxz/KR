using ProductAccounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProductAccounting.Pages
{
    /// <summary>
    /// Логика взаимодействия для WarehousesPage.xaml
    /// </summary>
    public partial class WarehousesPage : Page
    {
        public WarehousesPage()
        {
            InitializeComponent();
            LoadDataWarehouses();
        }
        private void LoadDataWarehouses()
        {
            using (var context = new ApplicationDbContext())
            {
                var warehouses = context.warehouses.ToList();
                warehousesGrid.ItemsSource = warehouses;
            }
        }

        private void CloseWarehousesPage(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;

        }
        public async void AddWarehouse(object sender, EventArgs e)
        {
            var window = new Window2();
            bool? dialogResult = window.ShowDialog();
            if(dialogResult == true && window.Result != null)
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Add(window.Result);
                    await context.SaveChangesAsync();
                }
            }
            RefreshDataGrid();

        }
        private void DeleteWarehouse(object sender, EventArgs e)
        {
            var selectedWarehouse = warehousesGrid.SelectedItem;
            if (selectedWarehouse != null)
            {
               
                var warehouse = (Models.Warehouses)selectedWarehouse;
                using (var context = new ApplicationDbContext())
                {
                    var itemToRemove = context.warehouses.FirstOrDefault(w => warehouse.id == w.id);
                    if (itemToRemove!=null)
                    {
                        context.warehouses.Remove(itemToRemove);
                        context.SaveChanges();
                      

                    }
                    else
                    {
                        MessageBox.Show("Элемент с данным ID не найден");
                    }
                    RefreshDataGrid();
                }
            }
        }

        private void RefreshDataGrid()
        {
            using(var context = new ApplicationDbContext()) 
            {
                warehousesGrid.ItemsSource = context.warehouses.ToList();
            }
        }



    }
}
