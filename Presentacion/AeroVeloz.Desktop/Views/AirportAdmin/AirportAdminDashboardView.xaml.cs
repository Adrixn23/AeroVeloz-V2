using System.Windows.Controls;
namespace AeroVeloz.Desktop.Views.AirportAdmin
{
    public partial class AirportAdminDashboardView : UserControl
    {
        public AirportAdminDashboardView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ViewModels.AirportAdmin.AirportAdminDashboardViewModel vm)
            {
                vm.LoadStatsCommand.Execute(null);
            }
        }
    }
}

