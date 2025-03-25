using System;
using System.Drawing;
using AvaApp1.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace AvaApp1.Controls;


public partial class RGBLamp : UserControl
{
    public RGBLamp()
    {
        DataContext = this;
        InitializeComponent();
        Ellipse ellipse;
    }

    static RGBLamp()
    {
        LampColorProperty = AvaloniaProperty.Register<RGBLamp, RGBColor?>(nameof(LampColor), coerce: OnLampColorChanged);
    }

    private static RGBColor? OnLampColorChanged(AvaloniaObject obj, RGBColor? nullable)
    {
        if (obj is RGBLamp rgb)
        {
            rgb.color.Fill = new SolidColorBrush(nullable);
            rgb.txt.Content= nullable.ToString();
        }

        return nullable;
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
     
        if (change.Property == LampColorProperty)
        {
            var tmp = change.Property;
        }
    }

    public static readonly StyledProperty<RGBColor?> LampColorProperty;

    public RGBColor? LampColor
    {
        get { return GetValue(LampColorProperty); }
        set
        {
            SetValue(LampColorProperty, value);

        }
    }



}
