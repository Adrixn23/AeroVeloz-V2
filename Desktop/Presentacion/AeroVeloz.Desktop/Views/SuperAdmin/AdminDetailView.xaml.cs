using System.Windows;
using System.Windows.Controls;
using AeroVeloz.Desktop.ViewModels.SuperAdmin;

namespace AeroVeloz.Desktop.Views.SuperAdmin;

public partial class AdminDetailView : UserControl
{
    public AdminDetailView()
    {
        InitializeComponent();
        this.DataContextChanged += AdminDetailView_DataContextChanged;
    }

    private void AdminDetailView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is AdminDetailViewModel vm)
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
        if (DataContext is AdminDetailViewModel vm && sender is PasswordBox pb)
        {
            vm.Password = pb.Password;
        }
    }
}
