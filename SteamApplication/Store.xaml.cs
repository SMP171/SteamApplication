using DataAccessLibrary;
using DataAccessLibrary.EntityFramework;
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

namespace SteamApplication
{
    /// <summary>
    /// Логика взаимодействия для Store.xaml
    /// </summary>
    public partial class Store : Page
    {
        private Frame MainFrame { get; set; }
        public ProductService Service { get; private set; }
        public Store(Frame frame)
        {
            InitializeComponent();
            Service = new ProductService();
            StoreDataGrid.ItemsSource = Service.SelectAllProducts();
            MainFrame = frame;
            Width = frame.Width;
            Height = frame.Height;
        }

        private void ProductInfo(object sender, RoutedEventArgs e)
        {
            product selectedProduct = new product();
            selectedProduct = StoreDataGrid.Items[StoreDataGrid.SelectedIndex] as product;
            MainFrame.NavigationService.Navigate(new ProductInfo(MainFrame, selectedProduct));
        }
    }
}
