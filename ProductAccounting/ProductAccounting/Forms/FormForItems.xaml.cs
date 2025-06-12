using ProductAccounting.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для FormForItems.xaml
    /// </summary>
    
    public class ItemDTO
    {
        public string _name { get; set; }
        public string _producttype { get; set; }
        public string _color { get; set; }
        public double _price { get; set; }
        public double _magnitude { get; set; }
        public string _unit { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public string ProducTtype
        {
            get => _producttype;
            set
            {
                _producttype = value;
            }
        }
        public string Color
        {
            get => _color;
            set
            {
                _color = value;
            }
        }
        public double Price
        {
            get => _price;
            set
            {
                _price = value;
            }
        }
        public double Magnitude
        {
            get => _magnitude;
            set
            {
                _magnitude = value;
            }
        }
        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
            }
        }
    }
    public class ItemUpdateDTO:INotifyPropertyChanged
    {
        public string _name { get; set; }
        public string _producttype { get; set; }
        public string _color { get; set; }
        public double _price { get; set; }
        public double _magnitude { get; set; }
        public string _unit { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public string ProducTtype
        {
            get => _producttype;
            set
            {
                _producttype = value;
                OnPropertyChanged();
            }
        }
        public string Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }
        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }
        public double Magnitude
        {
            get => _magnitude;
            set
            {
                _magnitude = value;
                OnPropertyChanged();
            }
        }
        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
    public static class ItemComparer
    {
        public static List<string> GetChangedFields(ItemDTO originalDTO, ItemUpdateDTO currentDTO)
        {
            List<string> changedFields = new List<string>();
            if (originalDTO.Name != currentDTO.Name) changedFields.Add(nameof(currentDTO.Name));
            if (originalDTO.ProducTtype != currentDTO.ProducTtype) changedFields.Add(nameof(currentDTO.ProducTtype));
            if (originalDTO.Color != currentDTO.Color) changedFields.Add(nameof(currentDTO.Color));
            if (originalDTO.Price != currentDTO.Price) changedFields.Add(nameof(currentDTO.Price));
            if (originalDTO.Magnitude != currentDTO.Magnitude) changedFields.Add(nameof(currentDTO.Magnitude));
            if (originalDTO.Unit != currentDTO.Unit) changedFields.Add(nameof(currentDTO.Unit));
            return changedFields;
        }
    }
    public partial class FormForItems : Window
    {
        ItemsController controller = new ItemsController();
        private ItemDTO _originalDTO;
        private ItemUpdateDTO _currentDTO;
        private bool _forCreate;
        private int _itemID;
        public FormForItems()
        {
            InitializeComponent();
            _currentDTO = new ItemUpdateDTO();
            this.DataContext = _currentDTO;
            _forCreate = true;
        }

        public FormForItems(int ID) 
        {
            InitializeComponent();
            _currentDTO = new ItemUpdateDTO();
            this.DataContext = _currentDTO;
            _itemID = ID;
            _forCreate = false;
            Loaded += async (s, e) =>
            {
                await LoadItemsAsync(ID);
            };
        }
        private async Task LoadItemsAsync(int ID)
        {
            _originalDTO = await controller.LoadDataItem(ID);
            NameItem.Text = _originalDTO.Name;
            TypeItem.Text = _originalDTO.ProducTtype;
            ColorItem.Text = _originalDTO.Color;
            PriceItem.Text = _originalDTO.Price.ToString(CultureInfo.InvariantCulture);
            MagnitudeItem.Text = _originalDTO.Magnitude.ToString();
            UnitItem.Text = _originalDTO.Unit;
        }
        private async void OkeyBtnItemtData(object sender, RoutedEventArgs e)
        {
            _currentDTO.Name = NameItem.Text;
            _currentDTO.ProducTtype = TypeItem.Text;
            _currentDTO.Color = ColorItem.Text;
            _currentDTO.Unit = UnitItem.Text;
            try
            {
                _currentDTO.Price = Convert.ToDouble(PriceItem.Text.Replace(".", ","));
                _currentDTO.Magnitude = Convert.ToDouble(MagnitudeItem.Text.Replace(".", ","));
            }
            catch (Exception ex) 
            {
                MessageBox.Show("В поля, принимающие чисовые значение были введены строковые");
            }
               
            
            if (!string.IsNullOrEmpty(_currentDTO.Name) 
                && !string.IsNullOrEmpty(_currentDTO.ProducTtype) 
                && !string.IsNullOrEmpty(_currentDTO.Color) 
                && _currentDTO.Price != 0
                && _currentDTO.Magnitude != 0 
                && !string.IsNullOrEmpty(_currentDTO.Unit))
            {
                if (_forCreate)
                {
                    bool success = await controller.AddItem(_currentDTO);
                    if (success)
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Произошла ошибка при добавлении");
                    }
                }
                else
                {
                    List<string> changes = ItemComparer.GetChangedFields(_originalDTO, _currentDTO);
                    if (changes.Count == 0)
                    {
                        MessageBox.Show("Нет изменений для сохранения");
                        return;
                    }
                    else
                    {
                        bool success = await controller.ChangeItem(_itemID, _currentDTO, changes);
                        if (success) DialogResult = true;
                        else MessageBox.Show("Ошибка при обновлении склада");
                        Logger.Log($"Пользователь внес изменения в справочник товары {DateTime.Now} \n");
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
