using ProductAccounting.Controllers;
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
        EmployeesController controller = new EmployeesController();
        public EmployeesPage()
        {
            InitializeComponent();
            InitializePage();
        }
        private async void InitializePage()
        {
            List<employees> loadedEmployees = await controller.LoadData();
            await employeersGrid.Dispatcher.InvokeAsync(() =>
            {
                employeersGrid.ItemsSource = loadedEmployees;
            });

        }

        private void CloseEmployeesPage(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        public async void DeleteEmployee(object sender, EventArgs e)
        {
            var selectedSuppliers = employeersGrid.SelectedItems;
            await controller.DeleteEmployee(selectedSuppliers);
            employeersGrid.ItemsSource = await controller.LoadData();
        }
        public async void AddEmployee(object sender, EventArgs e)
        {
            var winFormForSupplires = new FormForEmployees();
            bool? resultDialog = winFormForSupplires.ShowDialog();
            if (resultDialog == true)
            {
                employeersGrid.ItemsSource = await controller.LoadData();
            }
        }

        private async void ChangeSupplierBtn(object sender, EventArgs e)
        {
            employees selectedItem = (employees)employeersGrid.SelectedItem;
            if (selectedItem != null)
            {
                int selecyedIDItem = selectedItem.id;
                var winFormItems = new FormForEmployees(selecyedIDItem);
                bool? dialogResualt = winFormItems.ShowDialog();
                if (dialogResualt == true)
                {
                    employeersGrid.ItemsSource = await controller.LoadData();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите элемент");
            }

        }

        public void FindEmployee(object sender, EventArgs e)
        {
            FindForm findForm = new FindForm(employeersGrid);
            findForm.DataComboBox(DbFunctions.GetAllHeaders(employeersGrid));
            findForm.ShowDialog();

        }
    }
}
