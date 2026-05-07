using System.Windows;
using System.Windows.Controls;
using AeroVeloz.Desktop.ViewModels.SuperAdmin;

namespace AeroVeloz.Desktop.Views.SuperAdmin;

public partial class SuperAdminDashboardView : UserControl
{
    public SuperAdminDashboardView()
    {
        InitializeComponent();
    }

    private async void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is SuperAdminDashboardViewModel vm && vm.LoadStatsCommand.CanExecute(null))
        {
            await vm.LoadStatsCommand.ExecuteAsync(null);
        }
    }
}
