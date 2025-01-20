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
            LoadItemData();
        }

        private void LoadItemData()
        {
            using (var context = new ApplicationDbContext())
            {
                var items = context.items.ToList();
                ItemGrid.ItemsSource = items;
            }
        }

        private void CloseItemPage(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
