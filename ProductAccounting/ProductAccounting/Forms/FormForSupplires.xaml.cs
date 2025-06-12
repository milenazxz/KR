using ProductAccounting.Controllers;
using ProductAccounting.Models;
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

namespace ProductAccounting.Forms
{
    /// <summary>
    /// Логика взаимодействия для FormForSupplires.xaml
    /// </summary>
    /// 
    public class SupplierDTO
    {
        private string _name { get; set; }
        private string _organform { get; set; }
        private string _city { get; set; }
        private string  _address { get; set; }
        private int _rating { get; set; }
        private string _phonenumber { get; set; }
        private string _email { get; set; }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Organform
        {
            get => _organform;
            set => _organform = value;
        }

        public string City
        {
            get => _city; 
            set => _city = value;
        }
        public string Address
        {
            get => _address; 
            set => _address = value;
        }
        public int Rating
        {
            get => _rating; 
            set => _rating = value;
        }
        public string PhoneNumber
        {
            get => _phonenumber; 
            set => _phonenumber = value;
        }
        public string Email
        {
            get => _email; 
            set => _email = value;
        }
}

    public class SupplierUpdateDTO:INotifyPropertyChanged
    {
        private string _name { get; set; }
        private string _organform { get; set; }
        private string _city { get; set; }
        private string _address { get; set; }
        private int _rating { get; set; }
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
        public int Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                OnPropertyChanged();
            }
        }
        public string PhoneNumber
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

    public static class SupplierComparer
    {
        public static List<string> GetChangedFields(SupplierDTO originalDTO, SupplierUpdateDTO currentDTO)
        {
            List<string> changedFields = new List<string>();
            if (originalDTO.Name != currentDTO.Name) changedFields.Add(nameof(currentDTO.Name));
            if (originalDTO.Organform != currentDTO.Organform) changedFields.Add(nameof(currentDTO.Organform));
            if (originalDTO.City != currentDTO.City) changedFields.Add(nameof(currentDTO.City));
            if (originalDTO.Address != currentDTO.Address) changedFields.Add(nameof(currentDTO.Address));
            if (originalDTO.Rating != currentDTO.Rating) changedFields.Add(nameof(currentDTO.Rating));
            if (originalDTO.PhoneNumber != currentDTO.PhoneNumber) changedFields.Add(nameof(currentDTO.PhoneNumber));
            if (originalDTO.Email != currentDTO.Email) changedFields.Add(nameof(currentDTO.Email));
            return changedFields;
        }
    }

    public partial class FormForSupplires : Window
    {
        private SupplierDTO _originalDTO;
        private SupplierUpdateDTO _currentDTO;
        private int _idSupplier;
        private bool _forCreate;
        Regex _phoneRegex = new Regex(@"^(?:\+7|8)\d{10}$");

        SuppliresController controller = new SuppliresController();
        public FormForSupplires()
        {
            InitializeComponent();
            _currentDTO = new SupplierUpdateDTO();
            this.DataContext = _currentDTO;
            _forCreate = true;
        }

        public FormForSupplires(int ID)
        {
            InitializeComponent();
            Loaded += async (s, e) => 
            {
                await LoadSupplierData(ID);
            };

            _currentDTO = new SupplierUpdateDTO();
            this.DataContext = _currentDTO;
            _forCreate = false;
            _idSupplier = ID;
        }

        public async Task LoadSupplierData(int ID) 
        {
            _originalDTO = await controller.LoadDataSup(ID);
            _currentDTO = new SupplierUpdateDTO 
            {
                Name = _originalDTO.Name,
                Organform = _originalDTO.Organform,
                City = _originalDTO.City,
                Address = _originalDTO.Address,
                Rating = _originalDTO.Rating,
                PhoneNumber = _originalDTO.PhoneNumber,
                Email = _originalDTO.Email,
            };

            NameSupplire.Text = _currentDTO.Name;
            OrganformSupplire.Text = _currentDTO.Organform;
            CitySupplire.Text= _currentDTO.City;
            AddressSupplire.Text = _currentDTO.Address;
            RatingSupplire.Text = _currentDTO.Rating.ToString();
            PhoneNumberSupplire.Text = _currentDTO.PhoneNumber;
            EmailSupplire.Text = _currentDTO.Email;
        }

        public async void AddSuppliers(object sender, EventArgs e) 
        {
            _currentDTO.Name = NameSupplire.Text;
            _currentDTO.Organform = OrganformSupplire.Text;
            _currentDTO.City = CitySupplire.Text;
            _currentDTO.Address = AddressSupplire.Text;
            if (_currentDTO.Rating >= 11)
            {
                MessageBox.Show("Поле Рейтинг использует 10 бальную шкалу");
                return;
            }
            else
            {
                _currentDTO.Rating = Convert.ToInt32(RatingSupplire.Text);
            }
            _currentDTO.PhoneNumber = PhoneNumberSupplire.Text;
            if (!_phoneRegex.IsMatch(_currentDTO.PhoneNumber))
            {
                MessageBox.Show("Неверный формат номера телефона");
                return;
            }
            _currentDTO.Email = EmailSupplire.Text;

            if (!string.IsNullOrEmpty(_currentDTO.Name) 
                && !string.IsNullOrEmpty(_currentDTO.Organform) 
                && !string.IsNullOrEmpty(_currentDTO.City) 
                && !string.IsNullOrEmpty(_currentDTO.Address) 
                && _currentDTO.Rating != 0
                && !string.IsNullOrEmpty(_currentDTO.PhoneNumber) 
                && !string.IsNullOrEmpty(_currentDTO.Email))
            {
                if (_forCreate)
                {
                    bool succses = await controller.AddSupplier(_currentDTO);
                    if (succses)
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Возникла ошибка при добавлении поставщика");
                    }

                }
                else
                {
                    List<string> changes = SupplierComparer.GetChangedFields(_originalDTO, _currentDTO);
                    bool success = await controller.ChangeSupplier(_idSupplier, _currentDTO, changes);
                    if (success)
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при обновлении данных поставщика (возможно, поставщика с таким названием уже существует)");
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
