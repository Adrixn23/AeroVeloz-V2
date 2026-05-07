using System.Windows;
using System.Windows.Controls;
using AeroVeloz.Desktop.ViewModels.AirportAdmin;

namespace AeroVeloz.Desktop.Views.AirportAdmin
{
    public partial class UserDetailView : UserControl
    {
        public UserDetailView()
        {
            InitializeComponent();
            this.DataContextChanged += UserDetailView_DataContextChanged;
        }

        private void UserDetailView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is UserDetailViewModel vm)
            {
                vm.PropertyChanged += (s, args) =>
                {
                    if (args.PropertyName == nameof(vm.Password) && string.IsNullOrEmpty(vm.Password))
                    {
                        if (PasswordBoxControl.Password != string.Empty)
                        {
                            PasswordBoxControl.Password = string.Empty;
                        }
                    }
                };

                if (string.IsNullOrEmpty(vm.Password))
                {
                    PasswordBoxControl.Password = string.Empty;
                }
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserDetailViewModel vm && sender is PasswordBox pb)
            {
                vm.Password = pb.Password;
            }
        }
    }
}
