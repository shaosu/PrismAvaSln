using System;
using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;

namespace AvaApp1;

public partial class LightBeltViewer : UserControl
{
    Timer timer = new Timer(1000);
    public LightBeltViewer()
    {
        InitializeComponent();
        color1.LampColor = new Models.RGBColor(123, 145, 23);
        timer.Interval = 100;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    }
    ~LightBeltViewer()
    {
        timer.Stop();
    }
    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        byte[] bs = new byte[3];
        Random.Shared.NextBytes(bs);
        
        Dispatcher.UIThread.Invoke(new Action(() => {
            color1.LampColor = new Models.RGBColor(bs);
        }));
    }
}