using System.Windows.Controls;
using AeroVeloz.Desktop.ViewModels.AirportAdmin;

namespace AeroVeloz.Desktop.Views.AirportAdmin
{
    public partial class UserDetailView : UserControl
    {
        public UserDetailView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is UserDetailViewModel vm && sender is PasswordBox pb)
            {
                vm.Password = pb.Password;
            }
        }
    }
}
