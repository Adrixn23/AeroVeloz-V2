using System.Windows;
using System.Windows.Controls;
using AeroVeloz.Desktop.ViewModels.AirportAdmin;

namespace AeroVeloz.Desktop.Views.AirportAdmin;

public partial class AirlineListView : UserControl
{
    public AirlineListView()
    {
        InitializeComponent();
    }

    private async void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is AirlineListViewModel viewModel)
        {
            await viewModel.LoadConnectedAirlinesCommand.ExecuteAsync(null);
        }
    }
}
