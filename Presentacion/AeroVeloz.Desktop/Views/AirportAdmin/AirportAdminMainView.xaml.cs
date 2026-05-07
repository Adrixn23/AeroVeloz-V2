using System.Windows;
using AeroVeloz.Desktop.ViewModels.AirportAdmin;

namespace AeroVeloz.Desktop.Views.AirportAdmin
{
    public partial class AirportAdminMainView : Window
    {
        public AirportAdminMainView()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                if (DataContext is AirportAdminMainViewModel mainVm)
                {
                    // Pass the main view model to the dashboard so it can navigate
                    if (mainVm.CurrentViewModel is AirportAdminDashboardViewModel dashboardVm)
                    {
                        dashboardVm.SetMainViewModel(mainVm);
                    }
                }
            };
        }
    }
}
