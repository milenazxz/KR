using ProductAccounting.Controllers;
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
    }
}
