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
    /// Логика взаимодействия для SuppliresPage.xaml
    /// </summary>
    public partial class SuppliresPage : Page
    {
        SuppliresController controller = new SuppliresController();
        public SuppliresPage()
        {
            InitializeComponent();
            InitializePage();
        }

        private async void InitializePage() 
        {
            List<suppliers> loadedEmployees = await controller.LoadData();
            await suppliresGrid.Dispatcher.InvokeAsync(() => 
            {
                suppliresGrid.ItemsSource = loadedEmployees;
            });
               
        }

        private void CloseSuppliresPage(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
        private async void DeleteSupplier(object sender, EventArgs e)
        {
            var selectedSuppliers = suppliresGrid.SelectedItems;
            if(selectedSuppliers.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите запись");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить запись", "Удаление", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    await controller.DeleteSupplier(selectedSuppliers);
                    suppliresGrid.ItemsSource = await controller.LoadData();
                    Logger.Log($"Пользователь удалил запись из справочника поставщики {DateTime.Now} \n");
                }
            }
        }
        private async void AddSupplier(object sender, EventArgs e)
        {
            var winFormForSupplires = new FormForSupplires();
            bool? resultDialog = winFormForSupplires.ShowDialog();
            if (resultDialog == true) 
            {
                suppliresGrid.ItemsSource = await controller.LoadData();
                Logger.Log($"Пользователь добавил запись в справочник поставщики {DateTime.Now} \n");
            }

        }

        private async void ChangeSupplierBtn(object sender, EventArgs e) 
        {
            suppliers selectedItem = (suppliers)suppliresGrid.SelectedItem;
            if (selectedItem != null)
            {
                int selecyedIDItem = selectedItem.id;
                var winFormItems = new FormForSupplires(selecyedIDItem);
                bool? dialogResualt = winFormItems.ShowDialog();
                if (dialogResualt == true)
                {
                    suppliresGrid.ItemsSource = await controller.LoadData();
                    Logger.Log($"Пользователь внес изменения в справочник поставщики {DateTime.Now} \n");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите элемент");
            }

        }

    }
}
