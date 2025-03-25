using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia;
using System.ComponentModel;
using AvaApp1.Models;

namespace AvaApp1.Converters;

public class RGBColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return null;
        if (value is RGBColor color)
        {
            return  new SolidColorBrush(color);
        }
        return AvaloniaProperty.UnsetValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return null;
        if (value is SolidColorBrush color)
        {
            return new RGBColor(color.Color);
        }
        return AvaloniaProperty.UnsetValue;

    }
}
