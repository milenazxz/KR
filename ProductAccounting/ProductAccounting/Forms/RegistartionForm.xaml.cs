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
using System.Windows.Shapes;

namespace ProductAccounting.Forms
{
    /// <summary>
    /// Логика взаимодействия для RegistartionForm.xaml
    /// </summary>
    public partial class RegistartionForm : Window
    {
        public RegistartionForm()
        {
            InitializeComponent();
        }

        private async void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = NameField.Text;
            string login = LoginField.Text;
            string password1 = PasswordField1.Password;
            string password2 = PasswordField2.Password;
            string role = RoleField.Text;
            string contacts = ContactsField.Text;

            UserController controller = new UserController(new ApplicationDbContext());
            if (password1 != password2)
            {
                MessageBox.Show("Пароли не совпадают");
            }
            else if (PasswordField1.Password == LoginField.Text)
            {
                MessageBox.Show("Пароль и логин не должны совпадать");
            }
            else 
            {
                await controller.Registration(login, password1, role, name,contacts);
            }
        }
    }
}
