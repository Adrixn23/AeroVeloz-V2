using System.Windows.Controls;
using AeroVeloz.Desktop.ViewModels.AirportAdmin;

namespace AeroVeloz.Desktop.Views.AirportAdmin
{
    public partial class OperationsListView : UserControl
    {
        public OperationsListView()
        {
            InitializeComponent();
        }

        private void RefreshButton_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is OperationsListViewModel vm)
            {
                vm.LoadOperationsCommand.Execute(null);
            }
        }
    }
}
