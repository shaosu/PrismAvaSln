using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using AvaApp1.Views;
using Prism.Commands;
using Prism.Navigation.Regions;
using Prism.Events;
using AvaApp1.Events;

namespace AvaApp1.ViewModels
{
    public class SidebarViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _ea;
        public SidebarViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;
            _ea = ea;
            Title = "Navigation";
            RandomCommand = new DelegateCommand(OnRandom);
            NavigationCommand = new AsyncDelegateCommand(OnNavigate);

            MenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem { Header = "Introduction" , Children =
                {
                    new MenuItem() { Header = "Getting Started", Children =
                    {
                        new MenuItem() { Header = "Code of Conduct", NavigationCommand=NavigationCommand },
                        new MenuItem() { Header = "How to Contribute" ,NavigationCommand=NavigationCommand},
                        new MenuItem() { Header = "Development Workflow", NavigationCommand=NavigationCommand},
                    }},
                    new MenuItem() { Header = "Design Principles", NavigationCommand = NavigationCommand},
                    new MenuItem() { Header = "Contributing", Children =
                    {
                        new MenuItem() { Header = "Code of Conduct" , NavigationCommand = NavigationCommand},
                        new MenuItem() { Header = "How to Contribute" , NavigationCommand = NavigationCommand},
                        new MenuItem() { Header = "Development Workflow" , NavigationCommand = NavigationCommand},
                    }},
                }},
                new MenuItem { Header = "Controls", IsSeparator = true},
                new RegionMenuItem { Header = "Dashboard"  ,RegionName=nameof(DashboardView), NavigationCommand=NavigationCommand},
                new RegionMenuItem { Header = "Settings", RegionName= nameof(SettingsView) ,NavigationCommand=NavigationCommand },
                new RegionMenuItem { Header = "ButtonGroup" , Status="New"},
            };
        }
        private MenuItem? _selectedMenuItem;

        public MenuItem? SelectedMenuItem
        {
            get => _selectedMenuItem;
            set => SetProperty(ref _selectedMenuItem, value);
        }

        public ObservableCollection<MenuItem> MenuItems { get; set; }

        public ICommand RandomCommand { get; set; }
        public ICommand NavigationCommand { get; set; }

        private void OnRandom()
        {
            var items = GetLeaves();
            var index = new Random().Next(items.Count);
            SelectedMenuItem = items[index];
        }

        private List<MenuItem> GetLeaves()
        {
            List<MenuItem> items = new();
            foreach (var item in MenuItems)
            {
                items.AddRange(item.GetLeaves());
            }

            return items;
        }

        private async Task OnNavigate()
        {
            if (_selectedMenuItem is RegionMenuItem)
            {
                var regionMenuItem = (RegionMenuItem)_selectedMenuItem;
                if (string.IsNullOrWhiteSpace(regionMenuItem.RegionName) == false)
                {
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, regionMenuItem.RegionName);
                    _ea.GetEvent<CodeFileChangedEvent>().Publish(new CodeDialogViewModel(regionMenuItem.RegionName));
                    return;
                }
            }
            else
            {
                //_regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(SettingsView));
            }
        }
    }
}
