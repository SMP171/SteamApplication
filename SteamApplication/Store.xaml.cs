using DataAccessLibrary;
using DataAccessLibrary.EntityFramework;
using DomainModel;
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

namespace SteamApplication
{
    /// <summary>
    /// Interaction logic for Store.xaml
    /// </summary>
    public partial class Store : Window
    {
        private ProductService service = new ProductService();

        private List<Product> productsToDisplay = new List<Product>();

        public Store()
        {
            InitializeComponent();
            productsToDisplay = service.SelectAllProducts();
            productsGrid.ItemsSource = productsToDisplay;
        }
    }
}
