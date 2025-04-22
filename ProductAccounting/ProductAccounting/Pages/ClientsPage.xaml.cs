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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        ClientsController controller = new ClientsController();
        public ClientsPage()
        {
            InitializeComponent();
            InitializePageAsync();
        }

        private async void InitializePageAsync()
        {
             List<Clients> loadedData = await controller.LoadData();
            await clientsGrid.Dispatcher.InvokeAsync(() =>
            {
                clientsGrid.ItemsSource = loadedData;
            });
        }

        private void CloseClientsPage(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private async void DeleteClient(object sender, EventArgs e)
        {
            var selectedClients = clientsGrid.SelectedItems;
            await controller.DeleteClient(selectedClients);
            clientsGrid.ItemsSource = await controller.RefreshAsync();
        }

        private async void ChangeClientBtn(object sender, EventArgs e) 
        {
            Clients selectedItem = (Clients)clientsGrid.SelectedItem;
            var dialogWin = new FormForClients(selectedItem.id);
            bool? resualtDialog = dialogWin.ShowDialog();
            if (resualtDialog == true) 
            {
                clientsGrid.ItemsSource = await controller.RefreshAsync();
            }
        }

        private async void AddClient(object sender, EventArgs e)
        {
            var winFormForClients = new FormForClients();
            bool? resualDalog = winFormForClients.ShowDialog();
            if (resualDalog == true) 
            {
                clientsGrid.ItemsSource = await controller.RefreshAsync();
            }

        }
    }
}
