using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using AvaApp1.Models;
using Avalonia.Threading;
using Prism.Mvvm;

namespace AvaApp1.ViewModels;


public class RGBColorPack : BindableBase
{
    private RGBColor _C1;
    public RGBColor C1
    {
        get { return _C1; }
        set
        {
            SetProperty(ref _C1, value);
        }
    }

    public RGBColorPack(RGBColor c)
    {
        C1 = c;
    }
    public override string ToString()
    {
        return $"R:{C1.R} G:{C1.G} B:{C1.B}";
    }
}

internal class LightBeltViewerViewModel : BindableBase
{
    Timer timer = new Timer(1000);
    public LightBeltViewerViewModel()
    {
        _LightBeltItems = new ObservableCollection<RGBColorPack>();
        byte[] bs = new byte[3];
        for (byte i = 1; i <= 5; i++)
        {
            Random.Shared.NextBytes(bs);
            LightBeltItems.Add(new RGBColorPack(new RGBColor(bs)));
        }
        _C1 = new RGBColor(255, 0, 0);
        timer.Interval = 100;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        byte[] bs = new byte[3];
        Random.Shared.NextBytes(bs);
        //Dispatcher.UIThread.Invoke(new Action(() => {
        _C1 = new Models.RGBColor(bs);
        for (int i = 0; i < _LightBeltItems.Count; i++)
        {
            bs = new byte[3];
            Random.Shared.NextBytes(bs);
            _LightBeltItems[i].C1 = new RGBColor(bs);
        }

        //}));
    }

    private RGBColor _C1;
    public RGBColor C1
    {
        get { return _C1; }
        set
        {
            SetProperty(ref _C1, value);
        }
    }


    private ObservableCollection<RGBColorPack> _LightBeltItems;
    public ObservableCollection<RGBColorPack> LightBeltItems
    {
        get { return _LightBeltItems; }
        set
        {
            SetProperty(ref _LightBeltItems, value);
        }
    }

}
