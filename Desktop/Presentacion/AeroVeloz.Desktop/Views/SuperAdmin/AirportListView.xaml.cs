using System.Windows;
using System.Windows.Controls;
using AeroVeloz.Desktop.ViewModels.SuperAdmin;

namespace AeroVeloz.Desktop.Views.SuperAdmin;

public partial class AirportListView : UserControl
{
    public AirportListView()
    {
        InitializeComponent();
    }

    private async void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is AirportListViewModel vm && vm.LoadAirportsCommand.CanExecute(null))
        {
            await vm.LoadAirportsCommand.ExecuteAsync(null);
        }
    }
}
