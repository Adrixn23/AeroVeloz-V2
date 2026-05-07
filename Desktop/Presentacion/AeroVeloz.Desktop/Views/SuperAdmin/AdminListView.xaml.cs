using System.Windows;
using System.Windows.Controls;
using AeroVeloz.Desktop.ViewModels.SuperAdmin;

namespace AeroVeloz.Desktop.Views.SuperAdmin;

public partial class AdminListView : UserControl
{
    public AdminListView()
    {
        InitializeComponent();
    }

    private async void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is AdminListViewModel vm && vm.LoadDataCommand.CanExecute(null))
        {
            await vm.LoadDataCommand.ExecuteAsync(null);
        }
    }
}
