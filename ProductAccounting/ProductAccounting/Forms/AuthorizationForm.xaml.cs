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
using System.Windows.Shapes;

namespace ProductAccounting.Forms
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationForm.xaml
    /// </summary>
    public partial class AuthorizationForm : Window
    {
        UserController controller = new UserController(new ApplicationDbContext());
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginField.Text;
            string Password = PasswordField.Password;
            string Role = RoleField.Text;
            if (!string.IsNullOrEmpty(login) || !string.IsNullOrEmpty(Password) || !string.IsNullOrEmpty(Role))
            {
                CurrentUserData result = controller.Authorization(login, Password, Role);
                if (result != null)
                { 
                   // MainWindow mainWindow = new MainWindow(result);
                    //mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Введен неверный пароль");
                }
            }
            else 
            {
                MessageBox.Show("Пожалуйста, заполните все поля ");
            }
        }
    }
}
