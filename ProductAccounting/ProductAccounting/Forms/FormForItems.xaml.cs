using ProductAccounting.Controllers;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для FormForItems.xaml
    /// </summary>
    
    public class ItemDTO
    {
        public string name { get; set; }
        public string producttype { get; set; }
        public string color { get; set; }
        public double price { get; set; }
        public double magnitude { get; set; }
        public string unit { get; set; }
    }
    public partial class FormForItems : Window
    {
        ItemsController controller = new ItemsController();
        public FormForItems()
        {
            InitializeComponent();
        }

        public FormForItems(int ID) 
        {
            InitializeComponent();
            Loaded += async (s, e) =>
            {
                await LoadItemsAsync(ID);
            };
        }
        private async Task LoadItemsAsync(int ID)
        {
            ItemDTO itemDTO = await controller.LoadDataItem(ID);
            NameItem.Text = itemDTO.name;
            TypeItem.Text = itemDTO.producttype;
            ColorItem.Text = itemDTO.color;
            PriceItem.Text = itemDTO.price.ToString();
            MagnitudeItem.Text = itemDTO.magnitude.ToString();
            UnitItem.Text = itemDTO.unit;
        }
        private async void AddItem_Click(object sender, RoutedEventArgs e)
        {
            string name = NameItem.Text;
            string producttype = TypeItem.Text;
            string color = ColorItem.Text;
            double price = Convert.ToDouble(PriceItem.Text);
            double magnitude = Convert.ToDouble(MagnitudeItem.Text);
            string unit = UnitItem.Text;

            if (!string.IsNullOrEmpty(name) & !string.IsNullOrEmpty(producttype) & !string.IsNullOrEmpty(color) & price != 0 & magnitude != 0 & !string.IsNullOrEmpty(unit))
            {
                bool success = await controller.AddItem(name, producttype, color, price, magnitude, unit);
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
                MessageBox.Show("Пожалуйста, заполните все поля формы");
            }

               
        }

       
    }

}
