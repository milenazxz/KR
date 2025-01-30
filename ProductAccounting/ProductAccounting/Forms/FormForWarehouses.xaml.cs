using ProductAccounting.Models;
using ProductAccounting.Pages;
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
    /// Логика взаимодействия для FormForWarehouses.xaml
    /// </summary>
    public partial class FormForWarehouses : Window
    {
        public Warehouses result { get; private set; }
        public FormForWarehouses()
        {
            InitializeComponent();
        }
        public async void AddWarehouseData(object sender, EventArgs e)
        {
            string Name = Namewarehouse.Text;
            string City = Citywarehouse.Text;
            string Address = Addresswarehouse.Text;
            string Head = Headwarhouse.Text;

            if (Name != null && City != null && Address != null && Head != null)
            {
                result = new Warehouses() { name = Name, city = City, address = Address };
                DialogResult = true;
                await DbFunctions.AddData<Warehouses>(result);

            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля формы");
            }
        }
    }
}
