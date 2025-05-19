using ProductAccounting.Controllers;
using ProductAccounting.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
    public class Itemsforsale : INotifyPropertyChanged
    {
        private int _id_item;
        private Items _idItemNavigation;
        private double _pricePerUnit;
        private string _unit;
        private int _quantity;
        private int _nds;

        public int id { get; set; }
        public int id_sale { get; set; }

        public int id_item
        {
            get => _id_item;
            set
            {
                if (_id_item != value)
                {
                    _id_item = value;
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
            set { _pricePerUnit = value; OnPropertyChanged(); }
        }

        public string Unit
        {
            get => _unit;
            set { _unit = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        public int NDS
        {
            get => _nds;
            set { _nds = value; OnPropertyChanged(); }
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
            }
        }
    }

    public partial class FormForSales : Window { 
       
        public SalesController controller = new SalesController();

        // Коллекция строк DataGrid
        public ObservableCollection<Itemsforsale> SalesItems { get; set; } = new ObservableCollection<Itemsforsale>();

        // Коллекция товаров для ComboBox в строках
        public ObservableCollection<Items> AvailableItems { get; set; } = new ObservableCollection<Items>();

        public FormForSales()
        {
            InitializeComponent();

            DataContext = this; // важно для биндинга

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
            SalesItems.Add(new Itemsforsale());
        }

    }
}
