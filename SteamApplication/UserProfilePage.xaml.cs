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
    /// Логика взаимодействия для UserProfilePage.xaml
    /// </summary>
    public partial class UserProfilePage : Page
    {
        public UserProfilePage()
        {
            InitializeComponent();
            lblUserName.Content = "User: " + AuthenticationService.CurrentUser.nickname;
        }

        private void btnFriends_Click(object sender, RoutedEventArgs e)
        {
            FriendsPage friendsPage = new FriendsPage();
            this.NavigationService.Navigate(friendsPage);
        }

        private void Messages_Click(object sender, RoutedEventArgs e)
        {
            MessagePage messagePage = new MessagePage();
            this.NavigationService.Navigate(messagePage);
        }
    }
}
