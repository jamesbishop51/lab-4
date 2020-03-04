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

namespace lab_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NORTHWNDEntities db = new NORTHWNDEntities();


        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // sets the list for the stock level list box.
           lbxStock.ItemsSource = Enum.GetNames(typeof(StockLevel));

            // query that will set the source for the suppliers list box.
            var query1 = from s in db.Suppliers
                         orderby s.CompanyName
                         select new
                         {

                         };
        }

        private void lbxStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lbxSuppliers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lbxCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public enum StockLevel { low, Normal, Overstocked }
        
    }
}
