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
            if (selectedClients.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите запись");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить запись", "Удаление", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    await controller.DeleteClient(selectedClients);
                    clientsGrid.ItemsSource = await controller.RefreshAsync();
                    Logger.Log($"Пользователь удалил запись из справочника клиенты {DateTime.Now} \n");
                }
            }
        }

        private async void ChangeClientBtn(object sender, EventArgs e) 
        {
            Clients selectedItem = (Clients)clientsGrid.SelectedItem;
            if(selectedItem != null)
            {
                var dialogWin = new FormForClients(selectedItem.id);
                bool? resualtDialog = dialogWin.ShowDialog();
                if (resualtDialog == true)
                {
                    clientsGrid.ItemsSource = await controller.RefreshAsync();
                    Logger.Log($"Пользователь внес изменения в справочник клиенты {DateTime.Now} \n");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста выберете запись");
            }
           
        }

        private async void AddClient(object sender, EventArgs e)
        {
            var winFormForClients = new FormForClients();
            bool? resualDalog = winFormForClients.ShowDialog();
            if (resualDalog == true) 
            {
                clientsGrid.ItemsSource = await controller.RefreshAsync();
                Logger.Log($"Пользователь добавил запись в справочник клиенты {DateTime.Now} \n");
            }

        }
    }
}
