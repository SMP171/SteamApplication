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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private user user;
        public MainWindow(user tmpUser)
        {
            InitializeComponent();
            user = tmpUser;
            btnUserProfile.Content = tmpUser.nickname;

            

            //FriendsWindow friendsWindow = new FriendsWindow();
            //friendsWindow.Show();

            //MessageWindow messageWindow = new MessageWindow();
            //messageWindow.Show();

        }

        private void btnUserProfile_Click(object sender, RoutedEventArgs e)
        {
            UserProfilePage userProfilePage = new UserProfilePage(user);
            myFrame.NavigationService.Navigate(userProfilePage);
        }
    }
}
