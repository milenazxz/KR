using ProductAccounting.Controllers;
using ProductAccounting.Forms;
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
        Window form = new FormForWarehouses();
        public WarehousesPage()
        {
            InitializeComponent();
            InitializePageAsync();
        }
        private async Task InitializePageAsync()
        {
            await DbFunctions.LoadDataAsync<Warehouses>(warehousesGrid, w => w.IdHeadNavigation);

            //form.Closed += async (sender, args) => await DbFunctions.RefreshAsync<Warehouses>(warehousesGrid);
        }

        private void CloseWarehousesPage(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;

        }
        public async void AddWarehouse(object sender, EventArgs e)
        {
            var window = new FormForWarehouses();
            bool? dialogResult = window.ShowDialog();
            
            if (dialogResult == true)
            {
                await DbFunctions.RefreshAsync<Warehouses>(warehousesGrid);
            }
        }
        private async void DeleteWarehouse(object sender, EventArgs e)
        {
            var controller = new WarehousesController();
            var selectedWarehouse =(Warehouses)warehousesGrid.SelectedItem;
            await controller.DeleteWarehouses(selectedWarehouse, w => w.id == selectedWarehouse.id);
            //await DbFunctions.DeleteItem(selectedWarehouse, warehousesGrid, w => w.id == selectedWarehouse.id);
            await DbFunctions.RefreshAsync<Warehouses>(warehousesGrid);

        }

    }
}
