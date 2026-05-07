using System.Windows.Controls;
using AeroVeloz.Desktop.ViewModels.AirportAdmin;

namespace AeroVeloz.Desktop.Views.AirportAdmin
{
    public partial class UserListView : UserControl
    {
        public UserListView()
        {
            InitializeComponent();
        }

        private void RefreshButton_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is UserListViewModel vm)
            {
                vm.LoadUsersCommand.Execute(null);
            }
        }
    }
}
