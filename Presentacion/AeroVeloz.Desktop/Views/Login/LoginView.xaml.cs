using System.Windows;
using AeroVeloz.Desktop.ViewModels;

namespace AeroVeloz.Desktop.Views;

public partial class LoginView : Window
{
    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }
}
