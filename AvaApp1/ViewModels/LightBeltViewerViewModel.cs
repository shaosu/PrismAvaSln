using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using AvaApp1.Models;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using DryIoc;
using Prism.Commands;
using Prism.Mvvm;

namespace AvaApp1.ViewModels;

internal class LightBeltViewerViewModel : BindableBase
{
    private MemoryStream? memory;
    private Timer timer = new Timer(1000);
    private DryIoc.IContainer container;


    public LightBeltViewerViewModel(DryIoc.IContainer con)
    {
        _LightBeltItems = new ObservableCollection<RGBColorPack>();
        LightCount = 500;
        Interval = 100;
        container = con;
        timer.Interval = Interval;
        timer.Elapsed += Timer_Elapsed;
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        if (memory == null) return;
        byte[] bs = new byte[3];
        for (int i = 0; i < _LightBeltItems.Count; i++)
        {
            int c = memory.Read(bs);
            if (_Gray)
            {
                byte g1 = (byte)((bs[0] + bs[1] << 2 + bs[2]) >> 2);
                byte g2 = (byte)((bs[0] * 30 + bs[1] * 59 + bs[2] * 11 + 50) / 100);
                byte g3 = (byte)((bs[0] * 38 + bs[1] * 75 + bs[2] * 15) >> 7);
                bs[0] = bs[1] = bs[2] = g3;
            }
            _LightBeltItems[i].C1 = new RGBColor(bs);
            if (c < 1)
            {
                memory.Position = 0;
            }
        }
        if (_Gray)
            GrayValue = bs[0];
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



    private bool _Gray;
    public bool Gray
    {
        get { return _Gray; }
        set
        {
            SetProperty(ref _Gray, value);
        }
    }


    private byte _GrayValue;
    public byte GrayValue
    {
        get { return _GrayValue; }
        set
        {
            SetProperty(ref _GrayValue, value);
        }
    }


    private int _Interval;
    public int Interval
    {
        get { return _Interval; }
        set
        {
            SetProperty(ref _Interval, value);
            timer.Interval = value;
        }
    }


    private int _LightCount;
    public int LightCount
    {
        get { return _LightCount; }
        set
        {
            SetProperty(ref _LightCount, value);
            if (value > LightBeltItems.Count)
            {
                byte[] bs = new byte[3];
                while (LightBeltItems.Count < value)
                {
                    Random.Shared.NextBytes(bs);
                    LightBeltItems.Add(new RGBColorPack(new RGBColor(bs)));
                }
            }
            if (value < LightBeltItems.Count)
            {
                int c = LightBeltItems.Count - value;
                for (int i = 0; i < c; i++)
                {
                    LightBeltItems.RemoveAt(LightBeltItems.Count - 1);
                }
            }
        }
    }


    private bool _Runing;
    public bool Runing
    {
        get { return _Runing; }
        set
        {
            SetProperty(ref _Runing, value);
        }
    }

    public AsyncDelegateCommand StartCommand => new AsyncDelegateCommand(StartCommand_Sub);
    private async Task StartCommand_Sub()
    {
        if (timer.Enabled)
        {
            timer.Stop();
        }
        else
        {
            await LoadFile();
            timer.Start();
        }
        Runing = timer.Enabled;
    }

    private async Task LoadFile()
    {
        TopLevel _target = container.Resolve<TopLevel>();
        var files = await _target.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "选择颜色文件",
            FileTypeFilter = new[] { FilePickerFileTypes.All }
        });
        FileStream? read = files.First().OpenReadAsync().Result as System.IO.FileStream;
        if (read is not null)
        {
            memory = new MemoryStream();
            read.CopyTo(memory);
            read.Close();
        }
        else
        {
            memory = new MemoryStream();
            memory.Write(new byte[] { 1, 255, 1 }, 0, 3);
        }
        memory.Position = 0;
    }

}
