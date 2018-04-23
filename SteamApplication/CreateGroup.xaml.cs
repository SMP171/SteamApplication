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
    /// Логика взаимодействия для CreateGroup.xaml
    /// </summary>
    public partial class CreateGroup : Window
    {
        private Groups GroupPage { get; set; }

        public CreateGroup(Groups groupsPage)
        {
            InitializeComponent();

            GroupPage = groupsPage;
        }

        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            GroupService service = new GroupService();

            if (String.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите название группы!");
                return;
            }

            if (service.FindGroupByName(NameBox.Text).Group_name != null)
            {
                MessageBox.Show("Группа с таким именем уже существует!");
                return;
            }

            group newGroup = new group()
            {
                Group_name = NameBox.Text,
                User_id = AuthenticationService.CurrentUser.user_id
            };

            service.CreateGroup(newGroup);
            GroupPage.UpdateGroupsGrid();
            Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
