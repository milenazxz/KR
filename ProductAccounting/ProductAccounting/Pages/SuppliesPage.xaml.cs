using ProductAccounting.Controllers;
using ProductAccounting.Forms;
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
    /// Логика взаимодействия для SuppliesPage.xaml
    /// </summary>
    public partial class SuppliesPage : Page
    {
        SuppliesController controller = new SuppliesController();
        public SuppliesPage()
        {
            InitializeComponent();
            InitializePage();
        }
        private async void InitializePage()
        {
            var loadedData = await controller.LoadData(sup => sup.IdEmpNavigation, sup => sup.IdSupNavigation, sup => sup.IdWarehNavigation);
            await suppliesGrid.Dispatcher.InvokeAsync(() =>
            {
                suppliesGrid.ItemsSource = loadedData;
            });
        }

        private async void DeleteSupplies(object sender, EventArgs e) 
        {
            var selectedItems = suppliesGrid.SelectedItems;
            if(selectedItems.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите запись");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить запись", "Удаление", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    await controller.DeleteItems(selectedItems);
                    suppliesGrid.ItemsSource = await controller.LoadData(sup => sup.IdEmpNavigation, sup => sup.IdSupNavigation, sup => sup.IdWarehNavigation);
                    Logger.Log($"Пользователь удалил запись из журнала поставки {DateTime.Now} \n");
                }
            }
        }

        private void CloseSuppliesPage(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void AddSaleSuppliesBtn_Click(object sender, RoutedEventArgs e)
        {
            var winFormSupplies = new FormForSupplies();
            bool? result = winFormSupplies.ShowDialog();

            if (result == true) 
            {
                InitializePage();
                Logger.Log($"Пользователь добавил запись в журнал поставки {DateTime.Now} \n");
            }
        }
    }
}
