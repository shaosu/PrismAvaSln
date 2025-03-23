using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using Irihi.Avalonia.Shared.Contracts;
using Ursa.Controls;
using Prism.Commands;

namespace AvaApp1;

public partial class CustomDemoDialogViewModel : DialogVMBase, IDialogContext
{
    public WindowNotificationManager? NotificationManager { get; set; }
    public WindowToastManager? ToastManager { get; set; }

    public CustomDemoDialogViewModel()
    {
        Cities =
        [
            "Shanghai", "Beijing", "Hulunbuir", "Shenzhen", "Hangzhou", "Nanjing", "Chengdu", "Wuhan", "Chongqing",
            "Suzhou", "Tianjin", "Xi'an", "Qingdao", "Dalian"
        ];
        OKCommand = new DelegateCommand(OK);
        CancelCommand = new DelegateCommand(Cancel);
        DialogCommand = new AsyncDelegateCommand(ShowDialog);
        ShowToastCommand = new DelegateCommand<object>(ShowToast);
        ShowNotificationCommand = new DelegateCommand<object>(ShowNotification);
    }

    public ObservableCollection<string> Cities { get; set; }

    public void Close()
    {
        RequestClose?.Invoke(this, null);
    }

    public event EventHandler<object?>? RequestClose;


    public ICommand ShowNotificationCommand { get; set; }
    public ICommand ShowToastCommand { get; set; }
    public ICommand OKCommand { get; set; }
    public ICommand CancelCommand { get; set; }
    public ICommand DialogCommand { get; set; }

    private void OK()
    {
        RequestClose?.Invoke(this, true);
    }

    private void Cancel()
    {
        RequestClose?.Invoke(this, false);
    }

    private async Task ShowDialog()
    {
        await OverlayDialog.ShowCustomModal<CustomDemoDialog, CustomDemoDialogViewModel, bool>(
            new CustomDemoDialogViewModel());
    }


    private void ShowToast(object obj)
    {
        ToastManager?.Show("This is a Toast message");
    }


    private void ShowNotification(object obj)
    {
        NotificationManager?.Show("This is a Notification message");
    }
}
