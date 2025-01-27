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
            
            FrameInformation.Navigate(new WarehousesPage());
            PageLayer.Visibility = Visibility.Visible;
        }

        private void Button_Click_Items(object sender, RoutedEventArgs e)
        {
            FrameInformation.Navigate(new ItemPage());
            PageLayer.Visibility = Visibility.Visible;
        }
        private void Button_Click_Clients(object sender, RoutedEventArgs e)
        {
            FrameInformation.Navigate(new ClientsPage());
            PageLayer.Visibility = Visibility.Visible;
        }

        private void Button_Click_Employees(object sender, RoutedEventArgs e)
        {
            FrameInformation.Navigate(new EmployeesPage());
            PageLayer.Visibility = Visibility.Visible;
        }
        private void Button_Click_Suppliers(object sender, RoutedEventArgs e)
        {
            FrameInformation.Navigate (new SuppliresPage());
            PageLayer.Visibility = Visibility.Visible;
        }


    }
}
