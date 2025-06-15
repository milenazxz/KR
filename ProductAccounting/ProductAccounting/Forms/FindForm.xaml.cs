using Microsoft.EntityFrameworkCore;
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
using System.Windows.Shapes;

namespace ProductAccounting.Forms
{
    /// <summary>
    /// Логика взаимодействия для FindForm.xaml
    /// </summary>
    public partial class FindForm : Window
    {
        DataGrid _grid;
        
        public FindForm(DataGrid grid)
        {
            _grid = grid;
            InitializeComponent();

        }

        public void DataComboBox(List<string> headers)
        {
           SearchСategory.ItemsSource = headers;
        }
       /* public void FindInfo(object sender, EventArgs e)
        {
            string value = FindValue.Text;
            string columnName = SearchСategory.Text;
            string tableName = "employees";
            DbFunctions.Search<employees>(columnName, tableName, value, _grid);
        }*/
    }
}
