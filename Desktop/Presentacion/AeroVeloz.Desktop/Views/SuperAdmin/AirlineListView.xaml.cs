using System.Windows.Controls;
using AeroVeloz.Desktop.ViewModels.SuperAdmin;

namespace AeroVeloz.Desktop.Views.SuperAdmin;

public partial class AirlineListView : UserControl
{
    public AirlineListView()
    {
        InitializeComponent();
    }

    private async void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (DataContext is AirlineListViewModel viewModel)
        {
            await viewModel.LoadAirlinesCommand.ExecuteAsync(null);
        }
    }
}