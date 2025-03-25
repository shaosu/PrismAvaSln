namespace AvaApp1.Models;

public class RGBColor
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }

    public RGBColor()
    {
        
    }
    public RGBColor(Avalonia.Media.Color color)
    {
        R = color.R;
        G = color.G;
        B = color.B;
    }
    public RGBColor(byte[] color)
    {
        R = color[0];
        G = color[1];
        B = color[2];
    }
    public RGBColor(byte R, byte G, byte B) 
    {
        this.R = R;
        this.G = G;
        this.B = B;
    }

    public static implicit operator RGBColor(Avalonia.Media.Color color)
    {
        return new RGBColor
        {
            R = color.R,
            G = color.G,
            B = color.B
        };
    }
    public static implicit operator Avalonia.Media.Color(RGBColor color)
    {
        return new Avalonia.Media.Color(255, color.R, color.G, color.B);
    }
    public override string ToString()
    {
        return $"R:{R} G:{G} B:{B}";
    }
}
