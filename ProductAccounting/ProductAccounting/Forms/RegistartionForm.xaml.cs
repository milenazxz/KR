using ProductAccounting.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ProductAccounting.Forms
{
    /// <summary>
    /// Логика взаимодействия для RegistartionForm.xaml
    /// </summary>
    public class UserDTO
    {
        private string _name { get; set; }
        private string _post { get; set; }
        private string _contacts { get; set; }
        private string _login { get; set; }

        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public string Post
        {
            get => _post;
            set => _post = value;
        }
        public string Contacts
        {
            get => _contacts;
            set => _contacts = value;
        }
        public string Login
        {
            get => _login;
            set => _login = value;
        }

    }

    public class UserUpdateDTO:INotifyPropertyChanged
    {
        private string _name { get; set; }
        private string _post { get; set; }
        private string _contacts { get; set; }
        private string _login { get; set; }

        public string Name
        {
            get => _name;
            set 
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Post
        {
            get => _post;
            set 
            {
                _post = value;
                OnPropertyChanged();
            }
        }
        public string Contacts
        {
            get => _contacts;
            set 
            {
                _contacts = value;
                OnPropertyChanged();
            } 
        }
        public string Login
        {
            get => _login;
            set 
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
    public static class UserComparer
    {
        public static List<string> GetChangedFields(UserDTO originalDTO, UserUpdateDTO currentDTO)
        {
            List<string> changedFields = new List<string>();
            if (originalDTO.Name != currentDTO.Name) changedFields.Add(nameof(currentDTO.Name));
            if (originalDTO.Post != currentDTO.Post) changedFields.Add(nameof(currentDTO.Post));
            if (originalDTO.Contacts != currentDTO.Contacts) changedFields.Add(nameof(currentDTO.Contacts));
            if (originalDTO.Login != currentDTO.Login) changedFields.Add(nameof(currentDTO.Login));
            return changedFields;
        }
    }
    public partial class RegistartionForm : Window
    {
        private UserDTO _originalDTO;
        private UserUpdateDTO _currentDTO;  
        private int _id_user;
        private bool _forCreating;
        private UserController controller = new UserController();
        public RegistartionForm()
        {
            InitializeComponent();
            _forCreating = true;
            _currentDTO = new UserUpdateDTO();
            this.DataContext = _currentDTO;
        }

        public RegistartionForm(int ID)
        {
            InitializeComponent();
            _id_user = ID;
            _forCreating = false;
            _currentDTO = new UserUpdateDTO();
            this.DataContext = _currentDTO;
            Loaded += async (s, e) =>
            {
                await LoadData(ID);
            };
        }

        private async Task LoadData(int ID) 
        {
            _originalDTO = await controller.LoadDataUser(ID);
            _currentDTO = new UserUpdateDTO
            {
                Name = _originalDTO.Name,
                Post = _originalDTO.Post,
                Contacts = _originalDTO.Contacts,
                Login = _originalDTO.Login,
            };
            NameField.Text = _originalDTO.Name;
            LoginField.Text = _originalDTO.Login;
            RoleField.Text = _originalDTO.Post;
            ContactsField.Text = _originalDTO.Contacts;
            PasswordField1.Visibility = Visibility.Hidden;
            PasswordField2.Visibility = Visibility.Hidden;
            Password1.Visibility = Visibility.Hidden;
            Password2.Visibility = Visibility.Hidden;

        }

        private async void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            _currentDTO.Name = NameField.Text;
            _currentDTO.Post = RoleField.Text;
            _currentDTO.Contacts = ContactsField.Text;
            _currentDTO.Login = LoginField.Text;
            string password1 = PasswordField1.Password;
            string password2 = PasswordField2.Password;

            if (_forCreating)
            {
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
                    bool st = await controller.Registration(password1, _currentDTO);
                    if(st == true)
                    {
                        this.Close();
                    }
                    
                }
            }
            else
            {
                List<string> changes = UserComparer.GetChangedFields(_originalDTO, _currentDTO);
                if(changes.Count == 0)
                {
                    MessageBox.Show("Нет данных для изменения");
                    return;
                }
                else
                {
                    bool success = await controller.ChangeDataUser(_id_user, _currentDTO, changes);
                    if (success) 
                    {
                        DialogResult = true;
                        this.Close();
                    }
                    else MessageBox.Show("Ошибка при обновлении данных пользователя");
                }

            }
          
           
        }
    }
}
