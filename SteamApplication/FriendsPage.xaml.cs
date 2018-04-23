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
    /// Логика взаимодействия для FriendsPage.xaml
    /// </summary>
    public partial class FriendsPage : Page
    {
        public FriendsPage()
        {
            InitializeComponent();

            FriendService friendService = new FriendService();
            var userFriends = friendService.GetUserFriends(AuthenticationService.CurrentUser);
            FriendsDataGrid.ItemsSource = userFriends;
        }

        private void Button_Click_Delete_Row(object sender, RoutedEventArgs e)
        {
            int id = (FriendsDataGrid.SelectedItem as user).user_id;

            FriendService friendService = new FriendService();

            using (SteamContext context = new SteamContext())
            {
                user tmp = (from user in context.Users where user.user_id == id select user).SingleOrDefault();

                friendService.DeleteFriends(tmp);
            }

            user user2 = new user() { user_id = 2, nickname = "EEE", register_date = new DateTime(1582, 10, 5), status_id = 1, IsDeleted = 0, password = "123456789", wallet_id = 1 };

            var userFriends = friendService.GetUserFriends(user2);
            FriendsDataGrid.ItemsSource = userFriends;
        }

        private void Button_Click_Info(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Send(object sender, RoutedEventArgs e)
        {
            user friendUser = FriendsDataGrid.SelectedItem as user;
            ChatWindow chatWindow = new ChatWindow(friendUser);
            chatWindow.ShowDialog();

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            UserProfile userProfilePage = new UserProfile(AuthenticationService.CurrentUser);
            this.NavigationService.Navigate(userProfilePage);
        }

        private void BtnAddFriend_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
