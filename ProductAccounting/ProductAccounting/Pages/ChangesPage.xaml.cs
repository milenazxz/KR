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

namespace ProductAccounting.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangesPage.xaml
    /// </summary>
    public partial class ChangesPage : Page
    {
        ChangesController controller = new ChangesController();
        public ChangesPage()
        {
            InitializeComponent();
            LoadData();
        }


        public async void LoadData()
        {
            string textChanges = await controller.LoadChanges();
            List<string> changes = textChanges.Split('\n').ToList<string>();
            await ChangesGrid.Dispatcher.InvokeAsync(() =>
            {
                ChangesGrid.ItemsSource = changes;
            });
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
