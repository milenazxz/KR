using ProductAccounting.Controllers;
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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        UserController controller = new UserController();
        public UsersPage()
        {
            InitializeComponent();
            InitializePageAsync();


        }

        private async void InitializePageAsync()
        {
            List<employees> loadedData = await controller.LoadDataUsers();
            await UsersGrid.Dispatcher.InvokeAsync(() =>
            {
                UsersGrid.ItemsSource = loadedData;
            });
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
