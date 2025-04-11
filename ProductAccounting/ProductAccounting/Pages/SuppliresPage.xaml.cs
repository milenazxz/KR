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
        public SuppliresPage()
        {
            InitializeComponent();
            //DbFunctions.LoadData<suppliers>(suppliresGrid);
        }

        private void CloseSuppliresPage(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
        private async void DeleteSupplier(object sender, EventArgs e)
        {
            var selectedSuppliers = suppliresGrid.SelectedItem as suppliers;
           // await DbFunctions.DeleteItem(selectedSuppliers, suppliresGrid, s => s.id == selectedSuppliers.id);
        }
        private void AddSupplier(object sender, EventArgs e)
        {
            var winFormForSupplires = new FormForSupplires();
            winFormForSupplires.ShowDialog();
        }

    }
}
