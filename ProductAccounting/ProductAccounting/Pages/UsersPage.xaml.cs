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

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            var winAddUser = new RegistartionForm();
            bool? result = winAddUser.ShowDialog();
            if(result == true) 
            {
                UsersGrid.ItemsSource = await controller.LoadDataUsers();
            }
        }
        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            var user = UsersGrid.SelectedItems;
            if(user.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите запись");
            }
            else
            {
                MessageBoxResult result1 = MessageBox.Show("Вы уверены, что хотите удалить запись", "Удаление", MessageBoxButton.OKCancel);
                if (result1 == MessageBoxResult.OK)
                {
                    bool? result = await controller.DeleteUser(user);
                    if (result == true)
                    {
                        UsersGrid.ItemsSource = await controller.LoadDataUsers();
                    }
                }  
            }
        }

        private async void Change_Click(object sender, RoutedEventArgs e)
        {
          
            employees item = (employees)UsersGrid.SelectedItem;
            if(item != null)
            {
                int selectedItemID = item.id;
                var winAddUser = new RegistartionForm(selectedItemID);
                bool? result = winAddUser.ShowDialog();
                if (result == true)
                {
                    UsersGrid.ItemsSource = await controller.LoadDataUsers();
                }
               
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите элемент");
            }
           
        }
    }
}
