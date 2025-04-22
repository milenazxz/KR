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
    /// Логика взаимодействия для ItemPage.xaml
    /// </summary>
    /// 
 
    public partial class ItemPage : Page
    {
        ItemsController controller = new ItemsController();
        public ItemPage()
        {
            InitializeComponent();
            InitializePageAsync();
        }

        private async void InitializePageAsync()
        {
            var loadedData = await controller.LoadData();
            await ItemsGrid.Dispatcher.InvokeAsync(() =>
            {
                ItemsGrid.ItemsSource = loadedData;
            });
        }


        private void CloseItemPage(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private async void DeleteItem(object sender, EventArgs e)
        {
            var selectedItems = ItemsGrid.SelectedItems;
            await controller.DeleteItem(selectedItems);
            ItemsGrid.ItemsSource = await controller.RefreshAsync();
        }

        private async void AddItem(object sender, EventArgs e)
        {
            var winFormForItems = new FormForItems();
            bool? dialogResult = winFormForItems.ShowDialog();
            if (dialogResult == true) 
            {
                ItemsGrid.ItemsSource = await controller.RefreshAsync();
            }
        }

        public async void ChangeItem(object sender, EventArgs e) 
        {
            var selectedItem = (Items)ItemsGrid.SelectedItem;
            if (selectedItem != null)
            {
                int selecyedIDItem = selectedItem.id;
                var winFormItems = new FormForItems(selecyedIDItem);
                bool? dialogResualt = winFormItems.ShowDialog();
                if (dialogResualt == true)
                {
                    ItemsGrid.ItemsSource = await controller.RefreshAsync();
                }
            }
            else 
            {
                MessageBox.Show("Пожалуйста, выберите элемент");
            }

           
        }

    }
}
