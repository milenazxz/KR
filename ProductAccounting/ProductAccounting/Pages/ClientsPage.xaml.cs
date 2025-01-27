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
        public ClientsPage()
        {
            InitializeComponent();
            DbFunctions.LoadData<Clients>(clientsGrid);
        }

        private void CloseClientsPage(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private async void DeleteClient(object sender, EventArgs e)
        {
            var selectedClient = clientsGrid.SelectedItem as Clients;
            await DbFunctions.DeleteItem(selectedClient, clientsGrid, c => c.id == selectedClient.id);
        }

        private void AddClient(object sender, EventArgs e)
        {

        }
    }
}
