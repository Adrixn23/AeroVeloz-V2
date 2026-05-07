using System.Windows;
using AeroVeloz.Desktop.ViewModels;
using AeroVeloz.Desktop.Behaviors;

namespace AeroVeloz.Desktop.Views;

public partial class LoginView : Window
{
    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;

        this.Loaded += (s, e) =>
        {
            PasswordBoxBehavior.AttachPassword(PasswordBox);
        };
    }
}
