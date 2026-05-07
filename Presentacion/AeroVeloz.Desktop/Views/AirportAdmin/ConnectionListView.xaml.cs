using System.Windows.Controls;
using AeroVeloz.Desktop.ViewModels.AirportAdmin;

namespace AeroVeloz.Desktop.Views.AirportAdmin
{
    public partial class ConnectionListView : UserControl
    {
        public ConnectionListView()
        {
            InitializeComponent();
        }

        private void RefreshButton_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ConnectionListViewModel vm)
            {
                vm.LoadConnectionsCommand.Execute(null);
            }
        }
    }
}
