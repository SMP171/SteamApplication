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
            FriendsWindow friendsWindow = new FriendsWindow(user);
            friendsWindow.ShowDialog();
        }

        private void Messages_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow messageWindow = new MessageWindow(user);
            messageWindow.ShowDialog();
        }
    }
}
