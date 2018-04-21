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
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private user user;
        private user friendUser;

        public ChatWindow(user tmpUser, user tmpFriendUser)
        {
            InitializeComponent();

            user = tmpUser;
            friendUser = tmpFriendUser;

            lblFriendName.Content = "Friend: " + friendUser.nickname;

            FriendMessageService friendMessageService = new FriendMessageService();
            string result = friendMessageService.GetChatBetweenTwoUsers(user, friendUser);

            txtBox2.Text = result;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (txtBox.Text != "")
            {
                friend_messages friend_Messages = new friend_messages() { user_id = user.user_id, friend_id = friendUser.user_id, message = txtBox.Text, send_date = DateTime.Now};


                FriendMessageService friendMessageService = new FriendMessageService();

                friendMessageService.CreateFriendMsg(friend_Messages);

                //FriendMessageService friendMessageService = new FriendMessageService();
                string result = friendMessageService.GetChatBetweenTwoUsers(user, friendUser);

                txtBox2.Text = result;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
