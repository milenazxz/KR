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
    /// Логика взаимодействия для FormForWriteOffs.xaml
    /// </summary>

    public class ItemForWriteOffDTO:INotifyPropertyChanged
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

        public string Unit 
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
                Id = IdItem.id;
                Unit = IdItem.unit;
                Price = IdItem.price;
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
        public ObservableCollection<ItemForWriteOffDTO> WriteOffItems { get; set; } = new ObservableCollection<ItemForWriteOffDTO>();

        public ObservableCollection<Items> AvailableItems { get; set; } = new ObservableCollection<Items>();
        public FormForWriteOffs()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += async (a, e) =>
            {
                await LoadDataForComboBox();
                await LoadAvailableItemsAsync();
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
        private async Task LoadAvailableItemsAsync()
        {
            var itemsFromDb = await controller.LoadItemsAsync(); // метод, возвращающий List<Items> из БД
            foreach (var item in itemsFromDb)
                AvailableItems.Add(item);
        }
        public void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            WriteOffItems.Add(new ItemForWriteOffDTO());
        }

        private void DeleteRowButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = WriteOffItemsGrid.SelectedItems.Cast<ItemForWriteOffDTO>().ToList();

            if (selectedItems.Count > 0)
            {
                foreach (var item in selectedItems)
                {
                    WriteOffItems.Remove(item);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите хотя бы одну строку для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public async void Button_ClickAddWriteOff(object sender, RoutedEventArgs e)
        {
            bool success = await GetAddWriteOff();

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

        public async Task<bool> GetAddWriteOff()
        {
            DateTime date = new DateTime(2023, 4, 12);
            int id_writeOff;
            try
            {
                date = (DateTime)datePicker1.SelectedDate;
            }
            catch
            {
                MessageBox.Show("Пожалуйста, заполните поле Дата");
                return false;
            }
            if (HeadwarhouseComboBox.SelectedValue is int SelectedHeadId && WarhouseComboBox.SelectedValue is int SelectedWarehouseId)
            {
                string DateForDoc = date.ToString("dd MM yyyy HH:mm:ss", new CultureInfo("ru-RU"));
                id_writeOff = await controller.AddWriteOff(SelectedHeadId, SelectedWarehouseId, DateForDoc);

                if (id_writeOff >= 0)
                {
                    await controller.AddItemsForWriteOff(id_writeOff, WriteOffItems);
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
