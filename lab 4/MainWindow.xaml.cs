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
                             SupplierName = s.CompanyName,
                             SupplierID = s.SupplierID,
                             Country = s.Country
                         };

            lbxSuppliers.ItemsSource = query1.ToList();

            var query2 = query1
                .OrderBy(s => s.Country)
                .Select(s => s.Country);

            var countries = query2.ToList();
            lbxCountries.ItemsSource = countries.Distinct();
        }

        private void lbxStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //stock levels
            var query = from p in db.Products
                        where p.UnitsInStock < 50
                        orderby p.ProductName
                        select p.ProductName;


            //getting the selected item in the list box.
            string selected = lbxStock.SelectedItem as string;

            switch (selected)
            {
                case "Low":
                    //low is selected by default because of the above query
                    break;

                case "Normal":
                    query = from p in db.Products
                            where p.UnitsInStock < 50 && p.UnitsInStock <= 100
                            orderby p.ProductName
                            select p.ProductName;
                    break;
                case "Overstocked":
                    query = from p in db.Products
                            where p.UnitsInStock > 50
                            orderby p.ProductName
                            select p.ProductName;
                    break;

            }
            lbxProducts.ItemsSource = query.ToList();




        }

        private void lbxSuppliers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //selected value path
            int SupplierId = Convert.ToInt32(lbxSuppliers.SelectedValue);

            var query = from p in db.Products
                        where p.SupplierID == SupplierId
                        orderby p.ProductName
                        select p.ProductName;

            lbxProducts.ItemsSource = query.ToList();



        }

        private void lbxCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string country = (string)(lbxCountries.SelectedValue);

            var query = from p in db.Products
                        where p.Supplier.Country == country
                        orderby p.ProductName
                        select p.ProductName;

            lbxProducts.ItemsSource = query.ToList();


        }
        #region enums
        public enum StockLevel { Low, Normal, Overstocked }
        #endregion enums
    }
}
