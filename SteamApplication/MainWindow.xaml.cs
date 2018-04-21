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
using DataAccessLibrary.EntityFramework;
using DataAccessLibrary;

namespace SteamApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnUserProfile.Content = AuthenticationService.CurrentUser.nickname;

        }

        private void btnUserProfile_Click(object sender, RoutedEventArgs e)
        {
            UserProfilePage userProfilePage = new UserProfilePage();
            myFrame.NavigationService.Navigate(userProfilePage);
        }
    }
}
