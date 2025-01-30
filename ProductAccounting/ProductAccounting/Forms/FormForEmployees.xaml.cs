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
    public partial class FormForEmployees : Window
    {
        public employees employer;
        public FormForEmployees()
        {
            InitializeComponent();
        }
        public async void AddEmployee(object sender, EventArgs e)
        {
            string Name = NameEmployee.Text;
            string Post = PostEmployee.Text;
            string Contact = ContacntEmployee.Text;

            employer = new employees {name = Name, post = Post, contacts = Contact};
            DialogResult = true;
            await DbFunctions.AddData<employees>(employer);
        }
    }

}
