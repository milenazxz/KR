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
    /// Логика взаимодействия для FormForClients.xaml
    /// </summary>
    public partial class FormForClients : Window
    {
        ClientsController controller = new ClientsController();
        public FormForClients()
        {
            InitializeComponent();
        }

        public FormForClients(int ID) 
        {
            InitializeComponent();
            Loaded += async (s, e) =>
            {
                await LoadClientData(ID);
            };
        }


        private async Task LoadClientData(int ID) 
        {
           var item = await controller.LoadDataClient(ID);
            NameClient.Text = item.name;
            OrganformClient.Text = item.organform;
            CityClient.Text = item.city;
            AddressClient.Text = item.address;
            PhoneNumberClient.Text = item.phonenumber;
            EmailClient.Text = item.email;
        }
        
        public async void AddEmployee (object sender, EventArgs e)
        {
            string name = NameClient.Text;
            string organform = OrganformClient.Text;
            string city = CityClient.Text;
            string address = AddressClient.Text;
            string phonenumber = PhoneNumberClient.Text;
            string email = EmailClient.Text;

            if (!string.IsNullOrEmpty(name) & !string.IsNullOrEmpty(organform) & !string.IsNullOrEmpty(city) & !string.IsNullOrEmpty(address) & !string.IsNullOrEmpty(phonenumber) & !string.IsNullOrEmpty(email))
            {
               bool succses =  await controller.AddClient(name, organform, city, address, phonenumber, email);
                if (succses) 
                {
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Возникла ошибка при добавлении клиента");
                }
            }
            else 
            {
                MessageBox.Show("Пожалуйста, заполните все поля формы");
            }
        }
    }
}
