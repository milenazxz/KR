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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProductAccounting.Forms
{
    /// <summary>
    /// Логика взаимодействия для WriteOffsPage.xaml
    /// </summary>
    public partial class WriteOffsPage : Page
    {
        WriteOffsController controller = new WriteOffsController();
        public WriteOffsPage()
        {
            InitializeComponent();
            InitializePage();
        }

        private async void InitializePage()
        {
            var loadedData = await controller.LoadData(sup => sup.IdEmpNavigation, sup => sup.IdWarehNavigation);
            await writeOffsGrid.Dispatcher.InvokeAsync(() =>
            {
                writeOffsGrid.ItemsSource = loadedData;
            });
        }

        private async void DeleteWriteOffs(object sender, EventArgs e)
        {
            var selectedItems = writeOffsGrid.SelectedItems;
            if(selectedItems.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите запись");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить запись", "Удаление", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    await controller.DeleteItems(selectedItems);
                    writeOffsGrid.ItemsSource = await controller.LoadData(sup => sup.IdEmpNavigation, sup => sup.IdWarehNavigation);
                }
            }
        }

        private void CloseWriteOffsPage(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void AddWriteOffsBtn_Click(object sender, RoutedEventArgs e)
        {
            var winFormForWriteOff = new FormForWriteOffs();
            bool? result = winFormForWriteOff.ShowDialog();
            
            if (result == true) 
            {
                InitializePage();
            }
        }
    }
}
