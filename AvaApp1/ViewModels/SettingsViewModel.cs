using System.Diagnostics;
using AvaApp1.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Navigation.Regions;

namespace AvaApp1.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;

        public SettingsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Title = "Settings";
        }

        public DelegateCommand CmdNavigateToChild => new(() =>
        {
            Debug.WriteLine("CmdNavigateToChild() - Navigating away...");
            var navParams = new NavigationParameters
            {
                { "key1", "Some text" },
                { "key2", 999 }
            };

            _regionManager.RequestNavigate(
                RegionNames.ContentRegion,
                nameof(SettingsSubView),
                navParams);
        });

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Debug.WriteLine("OnNavigatedFrom()");
            base.OnNavigatedFrom(navigationContext);
        }
    }
}
