using AvaApp1.ViewModels;

namespace AvaApp1;

public class DialogVMBase : ViewModelBase
{
    private string? _owner;
    public string? Owner
    {
        get { return _owner; }
        set
        {
            SetProperty(ref _owner, value);
        }
    }

    private string? _City;
    public string? City
    {
        get { return _City; }
        set
        {
            SetProperty(ref _City, value);
        }
    }

    private string _Target;
    public string Target
    {
        get { return _Target; }
        set
        {
            SetProperty(ref _Target, value);
        }
    }

    private string? _Department;
    public string? Department
    {
        get { return _Department; }
        set
        {
            SetProperty(ref _Department, value);
        }
    }

}
