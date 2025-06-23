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
    /// Логика взаимодействия для SalesPage.xaml
    /// </summary>
    public partial class SalesPage : Page
    {
        SalesController controller = new SalesController();
        public SalesPage()
        {
            InitializeComponent();
            InitializePage();
        }

        private async void InitializePage() 
        {
            var loadedData = await controller.LoadData(s => s.IdWarehNavigation, s => s.IdClientNavigation, s=>s.IdEmpNavigation);
            await salesGrid.Dispatcher.InvokeAsync(() =>
            {
                salesGrid.ItemsSource = loadedData;
            });
        }
        
        private void CloseSalesPage(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;

        }

        public void AddSale(object sender, EventArgs e) 
        {
            var winFormForSales = new FormForSales();
            bool? result = winFormForSales.ShowDialog();
            if (result == true) 
            {
                InitializePage();
                Logger.Log($"Пользователь добавил запись в журнал продажи {DateTime.Now} \n");
            }
        }

        private async void DeleteSaleBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = salesGrid.SelectedItems;
            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите запись");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить запись", "Удаление", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    await controller.DeleteItems(selectedItems);
                    salesGrid.ItemsSource = await controller.LoadData(s => s.IdWarehNavigation, s => s.IdClientNavigation, s => s.IdEmpNavigation);
                    Logger.Log($"Пользователь удалил запись из журнала продажи {DateTime.Now} \n");
                }
            }
        }
        private async void New_Dok_click(object sender, RoutedEventArgs e)
        {
            Sales selectedSale = (Sales)salesGrid.SelectedItem;
            if (selectedSale == null) 
            {
                MessageBox.Show("Пожалуйста, выберите запись");
            }
            else
            {
                bool staitnatnt = await controller.SaleXml(selectedSale.id);
                if(staitnatnt == true)
                {
                    MessageBox.Show("Сформированный документ находится в папке D:\\KR\\KR\\ProductAccounting\\ProductAccounting\\bin\\Debug");
                }
            }
            
        }
    }
}
