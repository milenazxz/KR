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
    /// Логика взаимодействия для ItemPage.xaml
    /// </summary>
    public partial class ItemPage : Page
    {
        public ItemPage()
        {
            InitializeComponent();
            DbFunctions.LoadData<Items>(ItemsGrid);
        }

        
        private void CloseItemPage(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private async void DeleteItem(object sender, EventArgs e)
        {
            var selectedItem = ItemsGrid.SelectedValue as Items;
            await DbFunctions.DeleteItem<Items>(selectedItem, ItemsGrid, i => i.id == selectedItem.id);
        }

        private void AddItem(object sender, EventArgs e)
        {
            var winFormForItems = new FormForItems();
            winFormForItems.ShowDialog();
        }

    }
}
