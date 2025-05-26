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
    /// Логика взаимодействия для FormForSupplies.xaml
    /// </summary>

    public class ItemsforSupply : INotifyPropertyChanged
    {
        private int _id;
        private int _quantity;
        private string _unit;
        private double _price;
        private int _nds;
        private double _total;
        private Items _idNavigation;

        public int Id 
        {
            get => _id;
            set 
            {
                if (_id != value) 
                {
                    _id = value;
                    OnPropertyChanged();
                    UpdateRelotedPropertes();
                }
            }

        }

        public int Quantity 
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged();
                    UpdateRelotedPropertes();
                    RecalculateTotal();
                }
            }
        }

        public string Unit 
        {
            get => _unit;
            set 
            {
                if (_unit != value) 
                {
                    _unit = value;
                    OnPropertyChanged();
                    UpdateRelotedPropertes();
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
                    OnPropertyChanged();
                    UpdateRelotedPropertes();
                    RecalculateTotal();
                }
            }
        }

        public int Nds 
        {
            get => _nds;
            set 
            {
                if (_nds != value) 
                {
                    _nds = value;
                    OnPropertyChanged();
                    RecalculateTotal();
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

        public Items IdNavigation
        {
            get => _idNavigation;
            set 
            {
                if (value != _idNavigation) 
                {
                    _idNavigation = value;
                    OnPropertyChanged();
                    UpdateRelotedPropertes();
                } 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        public void UpdateRelotedPropertes() 
        {
            if (_idNavigation != null) 
            {
                _id = _idNavigation.id;
                _unit = _idNavigation.unit;
                _price = _idNavigation.price;
            }
        }

        public void RecalculateTotal() 
        {
            Total = Quantity * Price + (Quantity * Price) * (Nds / 100.0);
        }

    }
    public partial class FormForSupplies : Window
    {
        SuppliesController controller = new SuppliesController();

        public ObservableCollection<ItemsforSupply> SuppliesItems { get; set; } = new ObservableCollection<ItemsforSupply>();

        public ObservableCollection<Items> AvailableItems { get; set; } = new ObservableCollection<Items> ();
        public FormForSupplies()
        {
            InitializeComponent();
            Loaded += async (s, e) =>
            {
               await LoadDataForComboBox();
            };
        }

        public async Task LoadDataForComboBox() 
        {
            HeadwarhouseComboBox.ItemsSource = await controller.LoadForComboBoxEmp();
            HeadwarhouseComboBox.DisplayMemberPath = "Value";
            HeadwarhouseComboBox.SelectedValuePath = "Key";

            WarhouseComboBox.ItemsSource = await controller.LoadForComboBoxWareh();
            WarhouseComboBox.DisplayMemberPath = "Value";
            WarhouseComboBox.SelectedValuePath = "Key";

            SupplierComboBox.ItemsSource = await controller.LoadForComboBoxSup();
            SupplierComboBox.DisplayMemberPath = "Value";
            SupplierComboBox.SelectedValuePath = "Key";
        }

        private void Button_ClickAddSup(object sender, RoutedEventArgs e)
        {
            GetAddSupply();
        }

        public async void GetAddSupply()
        {
            int id_supply;
            List<int> items_id = new List<int>();
            List<int> item_quantity = new List<int>();
            if (HeadwarhouseComboBox.SelectedValue is int SelectedHeadId && WarhouseComboBox.SelectedValue is int SelectedWarehouseId && SupplierComboBox.SelectedValue is int SelectedSupplierID)
            {
                DateTime date = (DateTime)datePicker1.SelectedDate;
                id_supply = await controller.AddSupply(SelectedHeadId, SelectedWarehouseId, SelectedSupplierID, date);

                if (id_supply >= 0)
                {
                    foreach (ItemsforSupply item in SuppliesItems)
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
                    await controller.AddItemsForSupply(id_supply, items_id, item_quantity);
                }
            }
            else
            {
                MessageBox.Show("Не все поля регистрации были заполнены");
            }

        }
    }
}
