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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SteamApplication
{
    /// <summary>
    /// Логика взаимодействия для GroupInfo.xaml
    /// </summary>
    public partial class GroupInfo : Page
    {
        private Frame MainFrame { get; set; }
        private group Group { get; set; }
        private List<user> Members { get; set; }
        private GroupService Service { get; set; }

        public GroupInfo(Frame frame, group group)
        {
            InitializeComponent();

            MainFrame = frame;
            Group = group;
            Service = new GroupService();

            // Без реализации авторизации работать не будет
            //if(Group.UserId == AuthenticationService.CurrentUser.Id)
            //{
            //    SubcribeButton.Visibility = Visibility.Collapsed;
            //}

            Members = Service.SelectGroupMembers(Group);
            UpdateUIData();
        }

        public void UpdateUIData()
        {
            GroupCommentsService service = new GroupCommentsService();

            GroupCommentsDataGrid.ItemsSource = service.SelectGroupComments(Group);
            MembersDataGrid.ItemsSource = Members;

            GroupNameLabel.Content = Group.Group_name;
            CreatedDateLabel.Content = $"Created date: {Group.Created_date.ToShortDateString()}";
            CreatorLabel.Content = $"Creator: " +
                $"{Members.SingleOrDefault(x => x.user_id == Group.User_id).nickname}";
            MembersCountLabel.Content = $"Members count: {Members.Count}";

            if (Members.Contains(AuthenticationService.CurrentUser))
                SubcribeButton.Content = "UNSUBSCRIBE";
            else
                SubcribeButton.Content = "SUBSCRIBE";
        }

        private void SubcribeButtonClick(object sender, RoutedEventArgs e)
        {
            if (Members.Contains(AuthenticationService.CurrentUser))
            {
                Service.DeleteUserFromGroup(Group, AuthenticationService.CurrentUser);
            }
            else
            {
                Service.AddUserToGroup(Group, AuthenticationService.CurrentUser);
            }
        }
    }
}
