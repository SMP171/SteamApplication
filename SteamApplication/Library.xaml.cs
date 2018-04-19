using DataAccessLibrary;
using DataAccessLibrary.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Library.xaml
    /// </summary>
    public partial class Library : Page
    {
        private Frame MainFrame { get; set; }
        private ProductService service = new ProductService();
        private List<users_products> userProducts = new List<users_products>();

        public Library(Frame frame)
        {
            InitializeComponent();

            userProducts = service.SelectAllUsersProducts();
            LibraryDataGrid.ItemsSource = userProducts;

            MainFrame = frame;
            Width = frame.Width;
            Height = frame.Height;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = MainFrame.Width;
            this.Height = MainFrame.Height;
        }
    }
}
