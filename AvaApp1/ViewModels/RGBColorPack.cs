using AvaApp1.Models;
using Prism.Mvvm;

namespace AvaApp1.ViewModels;

public class RGBColorPack : BindableBase
{
    private RGBColor? _C1;
    public RGBColor? C1
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
