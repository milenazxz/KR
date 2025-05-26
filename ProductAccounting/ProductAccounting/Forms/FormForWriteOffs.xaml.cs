using ProductAccounting.Controllers;
using ProductAccounting.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для FormForWriteOffs.xaml
    /// </summary>

    public class ItemForWriteOff:INotifyPropertyChanged
    {
        private int _id;
        private int _quantity;
        private string _unit;
        private double _price;
        private double _total;
        private Items _idItem;

        public int Id 
        {
            get => _id;
            set 
            {
                if (_id != value) 
                {
                    _id = value;
                    UpdateRelotedPropertes();
                    OnPropertyChanged();
                }
            }
        }

        public int Quantity 
        {
            get => _quantity;
            set
            {
                if (value != _quantity) 
                {
                    _quantity = value;
                    UpdateRelotedPropertes();
                    OnPropertyChanged();
                    Recalculate();
                } 
            }
        }

        public string Unite 
        {
            get => _unit;
            set 
            {
                if (_unit != value)
                {
                    _unit = value;
                    UpdateRelotedPropertes();
                    OnPropertyChanged();
                }
            }
        }

        public double Price 
        {
            get => _price;
            set
            {
                if (_price != value) 
                {
                    _price = value;
                    UpdateRelotedPropertes();
                    OnPropertyChanged();
                    Recalculate();
                }
            }
        }
        public double Total 
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged();
            }
        }

        public Items IdItem 
        {
            get => _idItem;
            set 
            {
                if (_idItem != value) 
                {
                    _idItem = value;
                    UpdateRelotedPropertes();
                    OnPropertyChanged();
                }
            }
        }

        public void UpdateRelotedPropertes() 
        {
            if (_idItem != null) 
            {
                _id = IdItem.id;
                _unit = IdItem.unit;
                _price = IdItem.price;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void Recalculate() 
        {
            Total = Quantity * Price;
        }
    }
    
    
    public partial class FormForWriteOffs : Window
    {
        WriteOffsController controller = new WriteOffsController();
        public ObservableCollection<ItemForWriteOff> WriteOffItems { get; set; } = new ObservableCollection<ItemForWriteOff>();

        public ObservableCollection<Items> AvailableItems { get; set; } = new ObservableCollection<Items>();
        public FormForWriteOffs()
        {
            InitializeComponent();
            Loaded += async (a, e) =>
            {
                await LoadDataForComboBox();
            };
        }

        public async Task LoadDataForComboBox() 
        {
            HeadwarhouseComboBox.ItemsSource = await controller.LoadComboBoxEmp();
            HeadwarhouseComboBox.DisplayMemberPath = "Value";
            HeadwarhouseComboBox.SelectedValuePath = "Key";

            WarhouseComboBox.ItemsSource = await controller.LoadComboBoxWare();
            WarhouseComboBox.DisplayMemberPath = "Value";
            WarhouseComboBox.SelectedValuePath = "Key";
        }

        public async void GetAddSupply()
        {
            int id_writeOff;
            List<int> items_id = new List<int>();
            List<int> item_quantity = new List<int>();
            if (HeadwarhouseComboBox.SelectedValue is int SelectedHeadId && WarhouseComboBox.SelectedValue is int SelectedWarehouseId)
            {
                DateTime date = (DateTime)datePicker1.SelectedDate;
                id_writeOff = await controller.AddWriteOff(SelectedHeadId, SelectedWarehouseId, date);

                if (id_writeOff >= 0)
                {
                    foreach (ItemForWriteOff item in WriteOffItems)
                    {
                        if (item.Id >= 0 && item.Quantity > 0)
                        {
                            items_id.Add(item.Id);
                            item_quantity.Add(item.Quantity);
                        }
                        else
                        {
                            MessageBox.Show("Не все поля товаров были заполнены");
                        }


                    }
                    await controller.AddItemsForWriteOff(id_writeOff, items_id, item_quantity);
                }
            }
            else
            {
                MessageBox.Show("Не все поля регистрации были заполнены");
            }

        }
    }
}
