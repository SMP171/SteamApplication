using DataAccessLibrary;
using System;
using System.Data.Common;
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
using DataAccessLibrary.EntityFramework;

namespace SteamApplication
{
    /// <summary>
    /// Логика взаимодействия для FriendsWindow.xaml
    /// </summary>
    public partial class FriendsWindow : Window
    {
        private user user;
        public FriendsWindow(user tmpUser)
        {
            InitializeComponent();

            //user user2 = new user() { user_id = 2, nickname= "EEE", register_date = new DateTime(1582, 10, 5), status_id = 1, IsDeleted = 0, password = "123456789", wallet_id = 1 };
            user = tmpUser;
            FriendService friendService = new FriendService();
            var userFriends = friendService.GetUserFriends(tmpUser);
            //FriendsDataGrid.ItemsSource = userFriends.Select(s => new { Value = s }).ToList();
            FriendsDataGrid.ItemsSource = userFriends;

            //DateTime someDate = new DateTime(1582, 10, 5);
            //user user1 = new DataAccessLibrary.EntityFramework.user() { user_id = 4, nickname = "AAA", register_date = someDate, status_id = 1, IsDeleted = 0, password = "123456789", wallet_id = 1 };
            //user user21 = new DataAccessLibrary.EntityFramework.user() { user_id = 5, nickname = "BBB", register_date = someDate, status_id = 1, IsDeleted = 0, password = "123456789", wallet_id = 1 };
            ////User user3 = new User() { Id = 12, Nickname = "CCC", RegisterDate = someDate, StatusId = 1, IsDeleted = false, Password = "123456789", WalletId = 1 };

            ////friendService.CreateFriendUser(user1, user2);
            //friendService.GetUserFriends(user1);

            //friendService.DeleteFriends(user1);
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
            ChatWindow chatWindow = new ChatWindow(user, friendUser);
            chatWindow.ShowDialog();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
