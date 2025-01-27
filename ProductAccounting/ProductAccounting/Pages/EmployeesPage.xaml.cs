using ProductAccounting.Forms;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProductAccounting.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
            DbFunctions.LoadData<employees>(employeersGrid);
        }

        private void CloseEmployeesPage(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        public async void DeleteEmployee(object sender, EventArgs e)
        {
            var selectedEmployee = employeersGrid.SelectedItem as employees;
            await DbFunctions.DeleteItem(selectedEmployee, employeersGrid, em => em.id == selectedEmployee.id);
        }
        public void AddEmployee(object sender, EventArgs e)
        {
            var winFormForEmployees = new FormForEmployees();
            winFormForEmployees.ShowDialog();
        }
    }
}
