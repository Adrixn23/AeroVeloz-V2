using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace AeroVeloz.Desktop.Services.Dialog;

public class DialogService : IDialogService
{
    private object CreateDialogContent(string message, string title, PackIconKind iconKind, Brush iconBrush, bool isConfirm)
    {
        var stackPanel = new StackPanel { Margin = new Thickness(24), MinWidth = 350 };

        var headerPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 16) };
        var icon = new PackIcon { Kind = iconKind, Width = 32, Height = 32, VerticalAlignment = VerticalAlignment.Center };
        
        if (iconBrush != null)
        {
            icon.Foreground = iconBrush;
        }

        var titleBlock = new TextBlock
        {
            Text = title,
            FontSize = 20,
            FontWeight = FontWeights.SemiBold,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(12, 0, 0, 0)
        };

        headerPanel.Children.Add(icon);
        headerPanel.Children.Add(titleBlock);

        var messageBlock = new TextBlock
        {
            Text = message,
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 0, 0, 24),
            FontSize = 15
        };

        var buttonPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };

        if (isConfirm)
        {
            var btnCancel = new Button
            {
                Content = "Cancelar",
                Style = Application.Current.FindResource("MaterialDesignOutlinedButton") as Style,
                Margin = new Thickness(0, 0, 8, 0),
                Command = DialogHost.CloseDialogCommand,
                CommandParameter = false
            };
            buttonPanel.Children.Add(btnCancel);

            var btnOk = new Button
            {
                Content = "Confirmar",
                Style = Application.Current.FindResource("MaterialDesignRaisedButton") as Style,
                Command = DialogHost.CloseDialogCommand,
                CommandParameter = true
            };
            buttonPanel.Children.Add(btnOk);
        }
        else
        {
            var btnOk = new Button
            {
                Content = "Aceptar",
                Style = Application.Current.FindResource("MaterialDesignRaisedButton") as Style,
                Command = DialogHost.CloseDialogCommand,
                CommandParameter = true
            };
            buttonPanel.Children.Add(btnOk);
        }

        stackPanel.Children.Add(headerPanel);
        stackPanel.Children.Add(messageBlock);
        stackPanel.Children.Add(buttonPanel);

        return stackPanel;
    }

    public async Task ShowErrorAsync(string message, string title = "Error")
    {
        var content = CreateDialogContent(message, title, PackIconKind.AlertCircle, Brushes.Red, false);
        try { await DialogHost.Show(content, "RootDialog"); } catch { MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error); }
    }

    public async Task ShowInfoAsync(string message, string title = "Información")
    {
        var hueBrush = Application.Current.TryFindResource("PrimaryHueMidBrush") as Brush ?? Brushes.DodgerBlue;
        var content = CreateDialogContent(message, title, PackIconKind.Information, hueBrush, false);
        try { await DialogHost.Show(content, "RootDialog"); } catch { MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information); }
    }

    public async Task ShowWarningAsync(string message, string title = "Advertencia")
    {
        var content = CreateDialogContent(message, title, PackIconKind.Alert, Brushes.Orange, false);
        try { await DialogHost.Show(content, "RootDialog"); } catch { MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning); }
    }

    public async Task<bool> ShowConfirmationAsync(string message, string title = "Confirmación")
    {
        var hueBrush = Application.Current.TryFindResource("PrimaryHueMidBrush") as Brush ?? Brushes.DodgerBlue;
        var content = CreateDialogContent(message, title, PackIconKind.HelpCircle, hueBrush, true);
        try 
        { 
            var result = await DialogHost.Show(content, "RootDialog"); 
            return result is bool b && b;
        } 
        catch 
        { 
            return MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }
    }
}
