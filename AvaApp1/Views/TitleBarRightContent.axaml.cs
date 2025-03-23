using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Ursa.Common;
using Ursa.Controls.Options;
using Ursa.Controls;
using AvaApp1.ViewModels;

namespace AvaApp1.Views;

public partial class TitleBarRightContent : UserControl
{
    public TitleBarRightContent()
    {
        InitializeComponent();
    }

    private async void OpenRepository(object? sender, RoutedEventArgs e)
    {
        var top = TopLevel.GetTopLevel(this);
        if (top is null) return;
        var launcher = top.Launcher;
        await launcher.LaunchUriAsync(new Uri("https://github.com/shaosu/PrismAvaSln"));
    }

    private async void OpenDrawer(object? sender, RoutedEventArgs e)
    {
        if (this.DataContext is TitleBarRightContentViewModel tvm)
        {
            var options = new DrawerOptions()
            {
                Position = Position.Right,
                Buttons = DialogButton.None,
                CanLightDismiss = true,
                IsCloseButtonVisible = false,
                Title = "",
                CanResize = true,
            };
            bool IsLocal = false;
            var hostId = "LocalHost";
            var vm = tvm._codeModel;
            await Drawer.ShowModal<CodeDialog, CodeDialogViewModel>(vm, hostId, options);
        }
    }
}
