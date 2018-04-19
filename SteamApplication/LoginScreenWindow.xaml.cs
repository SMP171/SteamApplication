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
using DataAccessLibrary.EntityFramework;

namespace SteamApplication
{
    /// <summary>
    /// Логика взаимодействия для LoginScreenWindow.xaml
    /// </summary>
    public partial class LoginScreenWindow : Window
    {
        public LoginScreenWindow()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var userName = txtBoxUserName.Text;
            var userPswrd = txtBoxPassword.Text;

            using (var db = new SteamContext())
            {
                var users = (from user in db.Users
                             where user.nickname == userName && user.password == userPswrd
                             select user).ToList();

                if (users.Count == 1)
                {
                    MainWindow mainWindow = new MainWindow(users[0]);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect enter");
                }
            }

        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            //AddUserWndw addUserWndw = new AddUserWndw();
            //addUserWndw.Show();
            //this.Close();
        }
    }
}
