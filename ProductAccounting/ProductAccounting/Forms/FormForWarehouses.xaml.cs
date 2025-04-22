using ProductAccounting.Controllers;
using ProductAccounting.Models;
using ProductAccounting.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public partial class FormForWarehouses : Window
    {
        WarehousesController controller = new WarehousesController();
        public FormForWarehouses()
        {
            InitializeComponent();
            _ = LoadComboBoxAsync();
        }
        public FormForWarehouses(int ID) 
        {
            InitializeComponent();
            Loaded += async (s, e) =>
            {
                await LoadWarehouseAsync(ID);
                await LoadComboBoxAsync();
            };
            
        }

        private async Task LoadWarehouseAsync(int ID)
        {
            var item = await controller.LoadDataWarehouse(ID);
            Namewarehouse.Text = item.name;
            Citywarehouse.Text = item.city;
            Addresswarehouse.Text = item.address;
            HeadwarhouseComboBox.SelectedValue = item.IdHeadNavigation;
        }

        private async Task LoadComboBoxAsync()
        {
            HeadwarhouseComboBox.ItemsSource = await controller.LoadForCombobox();
            HeadwarhouseComboBox.DisplayMemberPath = "Value";
            HeadwarhouseComboBox.SelectedValuePath = "Key";
        }
        public async void AddWarehouseData(object sender, EventArgs e)
        {
            string Name = Namewarehouse.Text;
            string City = Citywarehouse.Text;
            string Address = Addresswarehouse.Text;
            int Head = (int)HeadwarhouseComboBox.SelectedValue;

            if (!string.IsNullOrEmpty(Name) & !string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Address) && Head != 0)
            {
                bool success = await controller.AddWarehouse(Name, City, Address ,Head);
                if (success)
                {
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении склада (возможно, склад с таким названием уже существует)");
                }
               
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля формы");
            }
        }
    }
}
