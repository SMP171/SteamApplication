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
    /// Логика взаимодействия для UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        private user User { get; set; }
        private UserService Service { get; set; }

        public UserProfile(user user)
        {
            User = user;
            name.Text = user.nickname;
            registerDate.Text = user.register_date.Value.ToShortDateString();
            status.Text = Service.SelectUser(user).ToString();
            balance.Text = Service.SelectUserWalletUser(user).ToString();
            InitializeComponent();
        }

        private void FriendButton_Clik(object sender, RoutedEventArgs e)
        {
            //Friend friend= new Friend;
            //friend.Show();
            //или
            // Content = new Page();
        }

        private void GroupButton_Clik(object sender, RoutedEventArgs e)
        {
            //Friend friend= new Friend;
            //friend.Show();
            //или
            // Content = new Page();
        }

        private void ThredButton_Clik(object sender, RoutedEventArgs e)
        {
            //Friend friend= new Friend;
            //friend.Show();
            //или
            // Content = new Page();
        }

        private void MessageButton_Clik(object sender, RoutedEventArgs e)
        {
            //Friend friend= new Friend;
            //friend.Show();
            //или
            // Content = new Page();
        }

        private void OderButton_Clik(object sender, RoutedEventArgs e)
        {
            //Friend friend= new Friend;
            //friend.Show();
            //или
            // Content = new Page(); 
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            money.Visibility = Visibility.Visible;
            add.Visibility = Visibility.Visible;
        }

        private void AddMany_Click(object sender, RoutedEventArgs e)
        {
            int many;
            int.TryParse(balance.ToString(), out many);

            Service.AddWallet(User, many);
        }
    }
}
