using DataAccessLibrary;
using DataAccessLibrary.EntityFramework;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Groups.xaml
    /// </summary>
    public partial class Groups : Page
    {
        private Frame MainFrame { get; set; }

        public Groups(Frame frame)
        {
            InitializeComponent();
                              
            GroupService service = new GroupService();
            GroupsDataGrid.ItemsSource = new ObservableCollection<group>(service.SelectAllGroups());

            MainFrame = frame;
            Width = frame.Width;
            Height = frame.Height;
        }

        private void CreateGroupButton_Click(object sender, RoutedEventArgs e)
        {
            Window createGroupWindow = new CreateGroup();
            createGroupWindow.Show(); 
        }

        private void GroupInfo(object sender, RoutedEventArgs e)
        {
            group selectedGroup = GroupsDataGrid.Items[GroupsDataGrid.SelectedIndex] as group;
            MainFrame.NavigationService.Navigate(new GroupInfo(MainFrame, selectedGroup));
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = MainFrame.Width;
            this.Height = MainFrame.Height;
        }
    }
}
