using DataAccessLibrary;
using DataAccessLibrary.EntityFramework;
using DomainModel;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private UserService Service { get; set; }

        public Registration()
        {
            InitializeComponent();
            Service = new UserService();
        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            if (ValidateRegistration())
            {
                Service.CreateUsers(new User
                {
                    Nickname = NicknameText.Text,
                    Password = PasswordText.Text
                });

                // Моментальная аутентификация после создания нового пользователя
                using(var context = new SteamContext())
                {
                    AuthenticationService authService = new AuthenticationService();
                    authService.SignIn(context.Users.SingleOrDefault(x => x.nickname == NicknameText.Text));
                }

                Close();
            }
        }

        // Проверка вводимых полей
        public bool ValidateRegistration()
        {
            if (String.IsNullOrWhiteSpace(NicknameText.Text)
            || String.IsNullOrWhiteSpace(PasswordText.Text)
            || String.IsNullOrWhiteSpace(ConfirmPassText.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return false;
            }

            if (Service.FindUserByNickname(NicknameText.Text) != null)
            {
                MessageBox.Show("This Nickname is already registered :<");
                return false;
            }

            if(PasswordText.Text.Length < 8)
            {
                MessageBox.Show("Password lenght must be at least 8 symbols");
                return false;
            }

            if (PasswordText.Text != ConfirmPassText.Text)
            {
                MessageBox.Show("Passwords are not equal!");
                return false;
            }

            return true;
        }
    }
}
