using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Prism.Common;
using Ursa.Controls;
using AvaApp1.ViewModels;
using Avalonia.Controls.Primitives;
using Avalonia.VisualTree;

namespace AvaApp1;

public partial class CustomDemoDialog : UserControl
{
    private CustomDemoDialogViewModel? _viewModel;
    public CustomDemoDialog()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _viewModel = this.DataContext as CustomDemoDialogViewModel;
        var visualLayerManager = this.FindAncestorOfType<VisualLayerManager>();
        if (visualLayerManager is not null && _viewModel is not null)
        {
            _viewModel.NotificationManager = new WindowNotificationManager(visualLayerManager) { MaxItems = 3 };
            _viewModel.ToastManager = new WindowToastManager(visualLayerManager) { MaxItems = 3 };
        }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _viewModel?.NotificationManager?.Uninstall();
        _viewModel?.ToastManager?.Uninstall();
    }
}
