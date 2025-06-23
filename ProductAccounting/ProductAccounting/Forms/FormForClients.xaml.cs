using ProductAccounting.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
    /// Логика взаимодействия для FormForClients.xaml
    /// </summary>

    public class ClientDTO
    {
        private string _name { get; set; }
        private string _organform { get; set; }
        private string _city { get; set; }

        private string _address { get; set; }

        private string _phonenumber { get; set; }
        private string _email { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public string Organform
        {
            get => _organform;
            set
            {
                _organform = value;
            }
        }
        public string City
        {
            get => _city;
            set
            {
                _city = value;
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
            }
        }
        public string Phonenumber
        {
            get => _phonenumber;
            set
            {
                _phonenumber = value;
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
            }
        }
    }

    public class ClientUpdateDTO: INotifyPropertyChanged
    {
        private string _name { get; set; }
        private string _organform { get; set; }
        private string _city { get; set; }

        private string _address { get; set; }

        private string _phonenumber { get; set; }
        private string _email { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Organform
        {
            get => _organform;
            set
            {
                _organform = value;
                OnPropertyChanged();
            }
        }
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }
        public string Phonenumber
        {
            get => _phonenumber;
            set
            {
                _phonenumber = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
    public static class ClientComparer
    {
        public static List<string> GetChangedFields(ClientDTO originalDTO, ClientUpdateDTO currentDTO)
        {
            List<string> changedFields = new List<string>();
            if (originalDTO.Name != currentDTO.Name) changedFields.Add(nameof(currentDTO.Name));
            if (originalDTO.Organform != currentDTO.Organform) changedFields.Add(nameof(currentDTO.Organform));
            if (originalDTO.City != currentDTO.City) changedFields.Add(nameof(currentDTO.City));
            if (originalDTO.Address != currentDTO.Address) changedFields.Add(nameof(currentDTO.Address));
            if (originalDTO.Phonenumber != currentDTO.Phonenumber) changedFields.Add(nameof(currentDTO.Phonenumber));
            if (originalDTO.Email != currentDTO.Email) changedFields.Add(nameof(currentDTO.Email));
            return changedFields;
        }
    }
    public partial class FormForClients : Window
    {
        private ClientDTO _originalDTO;
        private ClientUpdateDTO _currentDTO;
        private bool _forCreate;
        private int IdClient;
        Regex _phoneRegex = new Regex(@"^(?:\+7|8)\d{10}$");

        ClientsController controller = new ClientsController();
        public FormForClients()
        {
            InitializeComponent();
            _currentDTO = new ClientUpdateDTO();
            this.DataContext = _currentDTO;
            _forCreate = true;
        }

        public FormForClients(int ID) 
        {
            InitializeComponent();
            _forCreate = false;
            IdClient = ID;
            _currentDTO = new ClientUpdateDTO();
            this.DataContext = _currentDTO;
            Loaded += async (s, e) =>
            {
                await LoadClientData(ID);
            };
        }


        private async Task LoadClientData(int ID) 
        {
            _originalDTO = await controller.LoadDataClient(ID);
            _currentDTO = new ClientUpdateDTO
            {
                Name = _originalDTO.Name,
                Organform = _originalDTO.Organform,
                City = _originalDTO.City,
                Address = _originalDTO.Address,
                Phonenumber = _originalDTO.Phonenumber,
                Email = _originalDTO.Email,
            };
            NameClient.Text = _currentDTO.Name;
            OrganformClient.Text = _currentDTO.Organform;
            CityClient.Text = _currentDTO.City;
            AddressClient.Text = _currentDTO.Address;
            PhoneNumberClient.Text = _currentDTO.Phonenumber;
            EmailClient.Text = _currentDTO.Email;
        }
        
        public async void OkeyBtnClientData(object sender, EventArgs e)
        {
            _currentDTO.Name = NameClient.Text;
            _currentDTO.Organform = OrganformClient.Text;
            _currentDTO.City = CityClient.Text;
            _currentDTO.Address = AddressClient.Text;
            _currentDTO.Phonenumber = PhoneNumberClient.Text;
            _currentDTO.Email = EmailClient.Text;
            if (!_phoneRegex.IsMatch(_currentDTO.Phonenumber))
            {
                MessageBox.Show("Неверный формат номера телефона");
                return;
            }

            if (!string.IsNullOrEmpty(_currentDTO.Name)
                &&!string.IsNullOrEmpty(_currentDTO.Organform) 
                &&!string.IsNullOrEmpty(_currentDTO.City) 
                && !string.IsNullOrEmpty(_currentDTO.Address) 
                && !string.IsNullOrEmpty(_currentDTO.Phonenumber)
                && !string.IsNullOrEmpty(_currentDTO.Email))
            {
                if (_forCreate)
                {
                    bool succses = await controller.AddClient(_currentDTO);
                    if (succses)
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Возникла ошибка при добавлении клиента");
                    }
                }
                else
                {
                    List<string> changes = ClientComparer.GetChangedFields(_originalDTO, _currentDTO);
                    if(changes.Count == 0)
                    {
                        MessageBox.Show("Нет изменений для сохранения");
                        return;
                    }
                    else
                    {
                        bool success = await controller.ChangeClient(IdClient, _currentDTO, changes);
                        if (success) DialogResult = true;
                        else MessageBox.Show("Ошибка при обновлении склада");
                    }
                }
                
              
            }
            else 
            {
                MessageBox.Show("Пожалуйста, заполните все поля формы");
            }
        }
    }
}
