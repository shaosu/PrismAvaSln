using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AvaApp1.Services;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Styling;
using DryIoc;
using Prism.Commands;
using Prism.Common;
using Ursa.Common;
using Ursa.Controls;
using Ursa.Controls.Options;

namespace AvaApp1.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly INotificationService _notification;

        private int _counter = 0;
        private int _listItemSelected = -1;
        private ObservableCollection<string> _listItems = new();
        private string _listItemText = string.Empty;
 
        public DashboardViewModel(INotificationService notifyService)
        {
            _notification = notifyService;
        }

        public DelegateCommand CmdAddItem => new(() =>
        {
            _counter++;

            // Insert items at the top of the list
            ListItems.Insert(0, $"Item Number: {_counter}");

            // Insert at the bottom
            // ListItems.Add($"Item Number: {_counter}");
        });

        public DelegateCommand CmdClearItems => new(ListItems.Clear);
        public DelegateCommand CmdNotification => new(async () =>
        {
            var options = new DialogOptions()
            {
                Title = "Test Dialog",
                Mode = DialogMode.Info,
                Button = DialogButton.OK,
                ShowInTaskBar = true,
                IsCloseButtonVisible = true,
                StartupLocation = WindowStartupLocation.CenterScreen,
                CanDragMove = true,
                CanResize = false,
                StyleClass = "",
            };
            options.Title = "AAAA";
            var vm = new DefaultDemoDialogViewModel();
            await Dialog.ShowModal<DefaultDemoDialog, DefaultDemoDialogViewModel>(vm, options: options);

            // Alternate OnClick action
            _notification.Show("Hello Prism!", "Notification Pop-up Message.", () =>
            {
                int a = 4;
                a += 2;
            });
        });

        public int ListItemSelected
        {
            get => _listItemSelected;
            set
            {
                SetProperty(ref _listItemSelected, value);

                if (value == -1)
                    return;

                ListItemText = ListItems[ListItemSelected];
            }
        }

        public string ListItemText { get => _listItemText; set => SetProperty(ref _listItemText, value); }

        public ObservableCollection<string> ListItems { get => _listItems; set => SetProperty(ref _listItems, value); }

        public AsyncDelegateCommand ShowCodeCommand => new AsyncDelegateCommand(ShowCodeCommand_Sub);
        private async Task ShowCodeCommand_Sub()
        {
            var options = new DrawerOptions()
            {
                Position = Position.Right,
                Buttons = DialogButton.None,
                CanLightDismiss = true,
                IsCloseButtonVisible = false,
                Title = Title,
                CanResize = true,
            };
            bool IsLocal = false;
            var hostId = "LocalHost";
            var vm = new CodeDialogViewModel();
            await Drawer.ShowModal<CodeDialog, CodeDialogViewModel>(vm, hostId, options);
        }

    }
}
