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
    
    public partial class ChatWindow : Window
    {
        private user friendUser;

        public ChatWindow(user tmpFriendUser)
        {
            InitializeComponent();

            friendUser = tmpFriendUser;

            lblFriendName.Content = "Friend: " + friendUser.nickname;

            FriendMessageService friendMessageService = new FriendMessageService();
            string result = friendMessageService.GetChatBetweenTwoUsers(AuthenticationService.CurrentUser, friendUser);

            txtBox2.Text = result;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (txtBox.Text != "")
            {
                friend_messages friend_Messages = new friend_messages() { user_id = AuthenticationService.CurrentUser.user_id, friend_id = friendUser.user_id, message = txtBox.Text, send_date = DateTime.Now};

                FriendMessageService friendMessageService = new FriendMessageService();

                friendMessageService.CreateFriendMsg(friend_Messages);

                string result = friendMessageService.GetChatBetweenTwoUsers(AuthenticationService.CurrentUser, friendUser);

                txtBox2.Text = result;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
