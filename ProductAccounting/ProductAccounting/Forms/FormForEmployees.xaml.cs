using ProductAccounting.Controllers;
using ProductAccounting.Models;
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
    /// Логика взаимодействия для FormForEmployees.xaml
    /// </summary>
    
    public class EmployeeDTO
    {
        public string name { get; set; }
        public string post { get; set; }
        public string contacts { get; set; }
    }
    public partial class FormForEmployees : Window
    {
        EmployeesController controller = new EmployeesController();
        public FormForEmployees()
        {
            InitializeComponent();
        }
        public FormForEmployees(int ID)
        {
            InitializeComponent();
            Loaded += async (s, e) =>
            {
                await LoadSupplierData(ID);
            };
        }
        public async Task LoadSupplierData(int ID)
        {
            EmployeeDTO employeeDTO = await controller.LoadDataEmp(ID);
            NameEmployee.Text = employeeDTO.name;
            PostEmployee.Text = employeeDTO.post;
            ContacntEmployee.Text = employeeDTO.contacts;
        }
        public async void AddEmployee(object sender, EventArgs e)
        {
            string name = NameEmployee.Text;
            string post = PostEmployee.Text;
            string contacts = ContacntEmployee.Text;
            
            if (!string.IsNullOrEmpty(name) & !string.IsNullOrEmpty(post) & !string.IsNullOrEmpty(contacts)) 
            {
                bool succses = await controller.AddEmp(name, post, contacts);
                if (succses)
                {
                    if (succses)
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Возникла ошибка при добавлении поставщика");
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


