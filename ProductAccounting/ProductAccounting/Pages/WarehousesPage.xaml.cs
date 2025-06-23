using ProductAccounting.Controllers;
using ProductAccounting.Forms;
using ProductAccounting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
        WarehousesController controller = new WarehousesController();
        public WarehousesPage()
        {
            InitializeComponent();
            InitializePageAsync();
        }
        private async void InitializePageAsync()
        {
            var loadedData = await controller.LoadData(w => w.IdHeadNavigation);
            await warehousesGrid.Dispatcher.InvokeAsync(() =>
            {
                warehousesGrid.ItemsSource = loadedData;
            });
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
                warehousesGrid.ItemsSource = await controller.RefreshAsync();
                Logger.Log($"Пользователь добавил запись в справочник склады {DateTime.Now} \n");
            }
        }
        private async void DeleteWarehouse(object sender, EventArgs e)
        {
            var selectedWarehouse = warehousesGrid.SelectedItems;
            if(selectedWarehouse.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите запись");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить запись", "Удаление", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    await controller.DeleteWarehouses(selectedWarehouse);
                    warehousesGrid.ItemsSource = await controller.RefreshAsync();
                    Logger.Log($"Пользователь запись в справочнике склады{DateTime.Now} \n");
                }
            }
        }

        private async void Changewarehouses_Click(object sender, RoutedEventArgs e)
        {
            int selectedItemID;
            var selectedItem = (Warehouses)warehousesGrid.SelectedItem;
            if(selectedItem != null)
            {
                selectedItemID = selectedItem.id;
                var window = new FormForWarehouses(selectedItemID);
                bool? dialogResult = window.ShowDialog();
                if (dialogResult == true)
                {
                    warehousesGrid.ItemsSource = await controller.RefreshAsync();
                    Logger.Log($"Пользователь внес изменения в справочник склады {DateTime.Now} \n");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста выберите запись");
            }
        }
            

            
    }
}
