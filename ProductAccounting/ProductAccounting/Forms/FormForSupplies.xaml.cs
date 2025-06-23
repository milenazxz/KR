using ProductAccounting.Controllers;
using ProductAccounting.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

    public class ItemsforSupplyDTO : INotifyPropertyChanged
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
                Id = _idNavigation.id;
                Unit = _idNavigation.unit;
                Price = _idNavigation.price;
            }
        }

        public void RecalculateTotal() 
        {
            Total = Math.Round(Quantity * Price + (Quantity * Price) * (Nds / 100.0),2);
        }

    }
    public partial class FormForSupplies : Window
    {
        SuppliesController controller = new SuppliesController();

        public ObservableCollection<ItemsforSupplyDTO> SuppliesItems { get; set; } = new ObservableCollection<ItemsforSupplyDTO>();

        public ObservableCollection<Items> AvailableItems { get; set; } = new ObservableCollection<Items> ();
        public FormForSupplies()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += async (s, e) =>
            {
               await LoadDataForComboBox();
               await LoadAvailableItemsAsync();
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
        private async Task LoadAvailableItemsAsync()
        {
            var itemsFromDb = await controller.LoadItemsAsync(); // метод, возвращающий List<Items> из БД
            foreach (var item in itemsFromDb)
                AvailableItems.Add(item);
        }
        private void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            SuppliesItems.Add(new ItemsforSupplyDTO());
        }
        private void DeleteRowButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = supplyItemsGrid.SelectedItems.Cast<ItemsforSupplyDTO>().ToList();

            if (selectedItems.Count > 0)
            {
                foreach (var item in selectedItems)
                {
                    SuppliesItems.Remove(item);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите хотя бы одну строку для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public async void Button_ClickAddSup(object sender, RoutedEventArgs e)
        {
            bool success = await GetAddSupply();

            if (success)
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                return;
            }
        }

        public async Task<bool> GetAddSupply()
        {
            int id_supply;
            DateTime date = new DateTime(2023, 4, 12);
            try
            {
                date = (DateTime)datePicker1.SelectedDate;
            }
            catch
            {
                MessageBox.Show("Пожалуйста, заполните поле Дата");
                return false;
            }
            if (HeadwarhouseComboBox.SelectedValue is int SelectedHeadId && WarhouseComboBox.SelectedValue is int SelectedWarehouseId && SupplierComboBox.SelectedValue is int SelectedSupplierID)
            {
                string DateForDoc = date.ToString("dd MM yyyy HH:mm:ss", new CultureInfo("ru-RU"));
                id_supply = await controller.AddSupply(SelectedHeadId, SelectedWarehouseId, SelectedSupplierID, DateForDoc);

                if (id_supply >= 0)
                {
                    await controller.AddItemsForSupply(id_supply, SuppliesItems);
                    return true;
                }
                return true;
            }
            else
            {
                MessageBox.Show("Не все поля регистрации были заполнены");
                return false;
            }

        }
    }
}
