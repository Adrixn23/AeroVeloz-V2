using System.Windows;
using System.Windows.Controls;

namespace AeroVeloz.Desktop.Behaviors;

public static class PasswordBoxBehavior
{
    private static bool _isUpdating = false;

    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.RegisterAttached(
            "Password",
            typeof(string),
            typeof(PasswordBoxBehavior),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordChanged));

    public static string GetPassword(DependencyObject obj)
    {
        return (string)obj.GetValue(PasswordProperty);
    }

    public static void SetPassword(DependencyObject obj, string value)
    {
        obj.SetValue(PasswordProperty, value);
    }

    private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PasswordBox passwordBox)
        {
            if (_isUpdating) return;

            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            if ((string)e.NewValue != passwordBox.Password)
            {
                passwordBox.Password = (string)e.NewValue ?? string.Empty;
            }

            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }
    }

    private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is PasswordBox passwordBox)
        {
            _isUpdating = true;
            SetPassword(passwordBox, passwordBox.Password);
            _isUpdating = false;
        }
    }

    public static void AttachPassword(PasswordBox passwordBox)
    {
        // Keep for backward compatibility with View code-behinds
        if (passwordBox != null)
        {
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }
    }
}

