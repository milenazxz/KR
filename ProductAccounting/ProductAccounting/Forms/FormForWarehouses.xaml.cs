using ProductAccounting.Controllers;
using ProductAccounting.Models;
using ProductAccounting.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
using System.Xml.Linq;

namespace ProductAccounting.Forms
{
    /// <summary>
    /// Логика взаимодействия для FormForWarehouses.xaml
    /// </summary>
    public class WarehouseDTO
    {
        private string _name { get; set; }
        private string _city { get; set; }
        private string _address { get; set; }
        private int? _id_haed { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                 _name = value;
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

        public int? Id_head
        {
            get => _id_haed;
            set
            {
                _id_haed = value;
            }
        }
      
    }
    public class WarehouseUpdateDTO : INotifyPropertyChanged
    {
        private string _name { get; set; }
        private string _city { get; set; }
        private string _address { get; set; }
        private int? _id_haed { get; set; }

        public string Name
        {
            get { return _name; }
            set 
            {
                _name = value;
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

        public int? Id_head
        {
            get => _id_haed;
            set
            {
                _id_haed = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    public static class WarehouseComparer
    {
        public static List<string> GetChangedFields (WarehouseDTO originalDTO, WarehouseUpdateDTO currentDTO)
        {
            List<string> changedFields = new List<string>();
            if (originalDTO.Name != currentDTO.Name) changedFields.Add(nameof(currentDTO.Name));
            if (originalDTO.City != currentDTO.City) changedFields.Add(nameof(currentDTO.City));
            if (originalDTO.Address != currentDTO.Address) changedFields.Add(nameof(currentDTO.Address));
            if (originalDTO.Id_head != currentDTO.Id_head) changedFields.Add(nameof(currentDTO.Id_head));
            return changedFields;
        }

    }
    public partial class FormForWarehouses : Window
    {
        private WarehouseDTO _originalWarehouseDTO;
        private WarehouseUpdateDTO _curreentWarehouseDTO;

        private int IdWareHouse;
        private bool _forCreate;
        WarehousesController controller = new WarehousesController();
       public FormForWarehouses()
        {
            InitializeComponent();
            _curreentWarehouseDTO = new WarehouseUpdateDTO();
            this.DataContext = _curreentWarehouseDTO;
            _forCreate = true;
        }
        public FormForWarehouses(int ID)
        {
            InitializeComponent();
            _forCreate = false;
            IdWareHouse = ID;

            _curreentWarehouseDTO = new WarehouseUpdateDTO();
            this.DataContext = _curreentWarehouseDTO;

            Loaded += async (s, e) =>
            {
                await LoadWarehouseAsync(ID);
                await LoadComboBoxAsync();
            };
        }

        private async Task LoadWarehouseAsync(int ID)
        {
            _originalWarehouseDTO = await controller.LoadDataWarehouse(ID);
            _curreentWarehouseDTO = new WarehouseUpdateDTO
            {
                Name = _originalWarehouseDTO.Name,
                City = _originalWarehouseDTO.City,
                Address = _originalWarehouseDTO.Address,
                Id_head = _originalWarehouseDTO.Id_head
            };

            Namewarehouse.Text = _curreentWarehouseDTO.Name;
            Citywarehouse.Text = _curreentWarehouseDTO.City;
            Addresswarehouse.Text = _curreentWarehouseDTO.Address;
            //HeadwarhouseComboBox.SelectedValue = _curreentWarehouseDTO.IdHeadNavigation;
        }

        private async Task LoadComboBoxAsync()
        {
            HeadwarhouseComboBox.ItemsSource = await controller.LoadForCombobox();
            HeadwarhouseComboBox.DisplayMemberPath = "Value";
            HeadwarhouseComboBox.SelectedValuePath = "Key";
        }
       
        public async void OkeyBtnWarehouseData(object sender, EventArgs e)
        {
            _curreentWarehouseDTO.Name = Namewarehouse.Text;
            _curreentWarehouseDTO.City = Citywarehouse.Text;
            _curreentWarehouseDTO.Address = Addresswarehouse.Text;
            _curreentWarehouseDTO.Id_head = HeadwarhouseComboBox.SelectedValue as int?;
            if (!string.IsNullOrEmpty(_curreentWarehouseDTO.Name)
                && !string.IsNullOrEmpty(_curreentWarehouseDTO.City)
                && !string.IsNullOrEmpty(_curreentWarehouseDTO.Address)
                && _curreentWarehouseDTO.Id_head.HasValue)
            {
                if (_forCreate)
                {
                    var newDto = new WarehouseDTO
                    {
                        Name = _curreentWarehouseDTO.Name,
                        City = _curreentWarehouseDTO.City,
                        Address = _curreentWarehouseDTO.Address,
                        Id_head = _curreentWarehouseDTO.Id_head
                    };

                    bool success = await controller.AddWarehouse(newDto);
                    if (success) DialogResult = true;
                    else MessageBox.Show("Ошибка при добавлении склада");
                }
                else
                {
                    List<string> changes = WarehouseComparer.GetChangedFields(_originalWarehouseDTO, _curreentWarehouseDTO);
                    if (changes.Count == 0)
                    {
                        MessageBox.Show("Нет изменений для сохранения");
                        return;
                    }

                    bool success = await controller.ChangeWarehouse(IdWareHouse, _curreentWarehouseDTO, changes);
                    if (success) DialogResult = true;
                    else MessageBox.Show("Ошибка при обновлении склада");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
            }

        }
    }
}
