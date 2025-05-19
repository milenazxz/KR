using ProductAccounting.Controllers;
using ProductAccounting.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
    public class UserInputData:INotifyPropertyChanged
    {
        private string _name { get; set; }
        private string _unit { get; set; }
        private int _quentity { get; set; }
        private double _price { get; set; }
        private int _nds { get; set; }
        public string name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(name));
                }
            }
        }
        public string unit
        {
            get => _unit;
            set
            {
                if (_unit != value)
                {
                    _unit = value;
                    OnPropertyChanged(nameof(unit));
                }
            }
        }
        public int quentity
        {
            get => _quentity;
            set
            {
                if (_quentity != value)
                {
                    _quentity = value;
                    OnPropertyChanged(nameof(quentity));
                    OnPropertyChanged(nameof(Total)); // Уведомляем об изменении Total
                }
            }
        }
        public double price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(price));
                    OnPropertyChanged(nameof(Total)); // Уведомляем об изменении Total
                }
            }
        }

        public int NDS
        {
            get => _nds;
            set
            {
                if (_nds != value)
                {
                    _nds = value;
                    OnPropertyChanged(nameof(NDS));
                    OnPropertyChanged(nameof(Total)); // Уведомляем об изменении Total
                }
            }
        }
        public double Total => quentity * price * (1 + NDS / 100.0);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class ItemsForCombobox
    {
      
    }
   
    public partial class FormForSales :  Window, INotifyPropertyChanged
    {
        private IEnumerable<KeyValuePair<int, string>> _availableItems;

        public IEnumerable<KeyValuePair<int, string>> AvailableItems
        {
            get => _availableItems;
            set
            {
                _availableItems = value;
                OnPropertyChanged(nameof(AvailableItems));
            }
        }



        public ObservableCollection<UserInputData> ImputItems { get; set; }
        public SalesController controller = new SalesController();
        public FormForSales()
        {
            InitializeComponent();
            DataContext = this;
            ImputItems = new ObservableCollection<UserInputData>();
            Loaded += async (s, e) =>
            {
                await LoadComboBoxAsync();
            };
        }

        public async Task LoadItemsComboBoxAsync() 
        {
            AvailableItems = await controller.LoadForComboBoxItem();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        private void AddRow(object sender, EventArgs e) 
        {
            ImputItems.Add(new UserInputData
            {
                name = "Default Product",
                unit = "шт.",
                quentity = 1,
                price = 0,
                NDS = 20
                });
        }
    }
}
