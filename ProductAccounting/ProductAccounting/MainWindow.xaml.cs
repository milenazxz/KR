using ProductAccounting.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProductAccounting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Warehouses(object sender, RoutedEventArgs e)
        {
            //MainContent.Visibility = Visibility.Hidden;
            PageLayer.Visibility = Visibility.Visible;
            FrameInformation.Navigate(new WarehousesPage());
        }

        private void Button_Click_Items(object sender, RoutedEventArgs e)
        {
            PageLayer.Visibility = Visibility.Visible;
            FrameInformation.Navigate(new ItemPage());
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Кнопка нажата");
        }


    }
}
