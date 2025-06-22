using ProductAccounting.Controllers;
using ProductAccounting.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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
    /// Логика взаимодействия для FormForSales.xaml
    /// </summary>
    /// 
    public class ItemsforsaleDTO : INotifyPropertyChanged
    {
        private int _id;
        private Items _idItemNavigation;
        private double _pricePerUnit;
        private string _unit;
        private int _quantity;
        private int _nds;
        private double _total;
        public int id { get; set; }
        public int id_sale { get; set; }

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                    UpdateRelatedProperties();
                }
            }
        }

        public Items IdItemNavigation
        {
            get => _idItemNavigation;
            set
            {
                if (_idItemNavigation != value)
                {
                    _idItemNavigation = value;
                    OnPropertyChanged();
                    UpdateRelatedProperties();
                }
            }
        }

        public double PricePerUnit
        {
            get => _pricePerUnit;
            set { _pricePerUnit = value; OnPropertyChanged(); RecalculateTotal(); }
        }

        public string Unit
        {
            get => _unit;
            set { _unit = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); RecalculateTotal(); }
        }

        public int NDS
        {
            get => _nds;
            set { _nds = value; OnPropertyChanged(); RecalculateTotal(); }
        }

        public double Total 
        {
            set 
            {
                _total = value;
                OnPropertyChanged();
            }
            get => _total;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void UpdateRelatedProperties()
        {
            if (_idItemNavigation != null)
            {
                PricePerUnit = _idItemNavigation.price;
                Unit = _idItemNavigation.unit;
                _id = _idItemNavigation.id;
            }
        }
        private void RecalculateTotal() 
        {
            Total = Quantity * PricePerUnit + (Quantity * PricePerUnit) * (NDS / 100.0);
        }

    }

    public partial class FormForSales : Window { 
       
        public SalesController controller = new SalesController();

        // Коллекция строк DataGrid
        public ObservableCollection<ItemsforsaleDTO> SalesItems { get; set; } = new ObservableCollection<ItemsforsaleDTO>();

        // Коллекция товаров для ComboBox в строках
        public ObservableCollection<Items> AvailableItems { get; set; } = new ObservableCollection<Items>();
       
        public FormForSales()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += async (s, e) =>
            {
                await LoadComboBoxAsync();
                await LoadAvailableItemsAsync();
            };
        }

        private async Task LoadComboBoxAsync()
        {
            HeadwarhouseComboBox.ItemsSource = await controller.LoadForComboboxEmp();
            HeadwarhouseComboBox.DisplayMemberPath = "Value";
            HeadwarhouseComboBox.SelectedValuePath = "Key";

            WarhouseComboBox.ItemsSource = await controller.LoadForComboboxWareh();
            WarhouseComboBox.DisplayMemberPath = "Value";
            WarhouseComboBox.SelectedValuePath = "Key";

            ClientComboBox.ItemsSource = await controller.LoadForComboboxClient();
            ClientComboBox.DisplayMemberPath = "Value";
            ClientComboBox.SelectedValuePath = "Key";

        }
        private async Task LoadAvailableItemsAsync()
        {
            var itemsFromDb = await controller.LoadItemsAsync(); // метод, возвращающий List<Items> из БД
            foreach (var item in itemsFromDb)
                AvailableItems.Add(item);
        }

        private void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            SalesItems.Add(new ItemsforsaleDTO());
        }

        private void DeleteRowButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = salesItemsGrid.SelectedItems.Cast<ItemsforsaleDTO>().ToList();

            if (selectedItems.Count > 0)
            {
                    foreach (var item in selectedItems)
                    {
                        SalesItems.Remove(item);
                    }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите хотя бы одну строку для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Btn_AddSale(object sender, RoutedEventArgs e)
        {
            GetAddSale();
            this.DialogResult = true;
            this.Close();
        }

        public async void GetAddSale() 
        {
            int id_sale;
            List<int> items_id = new List<int>();
            List<int> item_quantity = new List<int>();
            if (HeadwarhouseComboBox.SelectedValue is int SelectedHeadId && WarhouseComboBox.SelectedValue is int SelectedWarehouseId 
                && ClientComboBox.SelectedValue is int SelectedClientID)
            {
               
                DateTime date = (DateTime)datePicker1.SelectedDate;
                string DateForDoc = date.ToString("dd MM yyyy HH:mm:ss", new CultureInfo("ru-RU"));
                id_sale = await controller.AddSale(SelectedHeadId, SelectedWarehouseId, SelectedClientID, DateForDoc);
                
                if (id_sale >= 0) 
                {
                  /* foreach (Itemsforsale item in SalesItems) 
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
                       

                    }*/
                   await controller.AddItemsForSale(id_sale, SalesItems);

                }
            }
            else 
            {
                MessageBox.Show("Не все поля регистрации были заполнены");
            }

        }

        
    }
}
