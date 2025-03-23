using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Common;
using System.Windows.Input;
using Ursa.Controls;
using Prism.Mvvm;
using Prism.Commands;
using AvaApp1.Views;
using Prism.Navigation.Regions;

namespace AvaApp1.ViewModels;

public class MenuItem: BindableBase
{
    static Random r = new Random();

    public string? Status { get; set; }
    public string? Header { get; set; }
    public int IconIndex { get; set; }
    public bool IsSeparator { get; set; }
    public ICommand NavigationCommand { get; set; }

    public MenuItem()
    {
        NavigationCommand = new AsyncDelegateCommand(OnNavigate);
        IconIndex = r.Next(100);
    }

    private async Task OnNavigate()
    {
        await MessageBox.ShowOverlayAsync(Header ?? string.Empty, "Navigation Result");
    }

    public ObservableCollection<MenuItem> Children { get; set; } = new ObservableCollection<MenuItem>();

    public IEnumerable<MenuItem> GetLeaves()
    {
        if (this.Children.Count == 0)
        {
            yield return this;
            yield break;
        }

        foreach (var child in Children)
        {
            var items = child.GetLeaves();
            foreach (var item in items)
            {
                yield return item;
            }
        }
    }
}

public class RegionMenuItem: MenuItem
{
    public string? RegionName { get; set; }
}
