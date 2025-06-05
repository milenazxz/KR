using ProductAccounting.Controllers;
using ProductAccounting.Models;
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

namespace ProductAccounting.Forms
{
    /// <summary>
    /// Логика взаимодействия для FormForSupplires.xaml
    /// </summary>
    /// 
    public class SupplyerDTO
    {
        public string name { get; set; }
        public string organform { get; set; }
        public string city { get; set; }
        public string  address { get; set; }
        public int rating { get; set; }
        public string phonenumber { get; set; }
        public string email { get; set; }
}
    public partial class FormForSupplires : Window
    {
        private int IdSupplier;
        private bool _forCreate;

        SuppliresController controller = new SuppliresController();
        public FormForSupplires()
        {
            InitializeComponent();
            _forCreate = true;
        }

        public FormForSupplires(int ID)
        {
            InitializeComponent();
            Loaded += async (s, e) => 
            {
                await LoadSupplierData(ID);
            };
            _forCreate = false;
            IdSupplier = ID;
        }

        public async Task LoadSupplierData(int ID) 
        {
            SupplyerDTO supplier = await controller.LoadDataSup(ID);
            NameSupplire.Text = supplier.name;
            OrganformSupplire.Text = supplier.organform;
            CitySupplire.Text= supplier.city;
            AddressSupplire.Text = supplier.address;
            RatingSupplire.Text = supplier.rating.ToString();
            PhoneNumberSupplire.Text = supplier.phonenumber;
            EmailSupplire.Text = supplier.email;
        }

        public async void AddSuppliers(object sender, EventArgs e) 
        {
            string name = NameSupplire.Text;
            string organform = OrganformSupplire.Text;
            string city = CitySupplire.Text;
            string address = AddressSupplire.Text;
            int rating = Convert.ToInt32(RatingSupplire.Text);
            string phone = PhoneNumberSupplire.Text;
            string email = EmailSupplire.Text;

            if (!string.IsNullOrEmpty(name) & !string.IsNullOrEmpty(organform) & !string.IsNullOrEmpty(city) & !string.IsNullOrEmpty(address) & rating != 0 & !string.IsNullOrEmpty(phone) & !string.IsNullOrEmpty(email))
            {
                if (_forCreate)
                {
                    bool succses = await controller.AddSupplier(name, organform, city, address, rating, phone, email);
                    if (succses)
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Возникла ошибка при добавлении поставщика");
                    }

                }
                else
                {
                    bool success = await controller.ChangeSupplier(IdSupplier, name, organform, city, address, rating, phone, email);
                    if (success)
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при обновлении данных поставщика (возможно, поставщика с таким названием уже существует)");
                    }
                }
              
            }
            else 
            {
                MessageBox.Show("Пожалуйста, заполните все поля формы");
            }
            

        }
    }
}
