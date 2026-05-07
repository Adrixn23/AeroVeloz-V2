using CommunityToolkit.Mvvm.ComponentModel;

namespace AeroVeloz.Desktop.ViewModels.Base;

public partial class BaseViewModel : ObservableValidator
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    public bool IsNotBusy => !IsBusy;
}
