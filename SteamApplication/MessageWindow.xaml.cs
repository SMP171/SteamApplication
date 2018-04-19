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
using System.Windows.Shapes;

namespace SteamApplication
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow()
        {
            InitializeComponent();

            user user2 = new user() { user_id = 2, nickname = "EEE", register_date = new DateTime(1582, 10, 5), status_id = 1, IsDeleted = 0, password = "123456789", wallet_id = 1 };

            FriendService friendService = new FriendService();
            var userFriends = friendService.GetUserFriends(user2);
            //FriendsDataGrid.ItemsSource = userFriends.Select(s => new { Value = s }).ToList();
            FriendsDataGrid.ItemsSource = userFriends;
        }

        private void Button_Click_Chat(object sender, RoutedEventArgs e)
        {

        }
    }
}
