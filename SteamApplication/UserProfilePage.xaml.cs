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

namespace SteamApplication
{
    /// <summary>
    /// Логика взаимодействия для UserProfilePage.xaml
    /// </summary>
    public partial class UserProfilePage : Page
    {
        private user user;
        public UserProfilePage(user tmpUser)
        {
            InitializeComponent();
            user = tmpUser;
            lblUserName.Content = "User: " + user.nickname;
        }

        private void btnFriends_Click(object sender, RoutedEventArgs e)
        {
            FriendsPage friendsPage = new FriendsPage(user);
            this.NavigationService.Navigate(friendsPage);
        }

        private void Messages_Click(object sender, RoutedEventArgs e)
        {
            MessagePage messagePage = new MessagePage(user);
            this.NavigationService.Navigate(messagePage);
        }
    }
}
