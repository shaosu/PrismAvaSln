using System;

namespace AvaApp1;

public class CodeDialogViewModel : DialogVMBase
{
    public string AXAML { get; set; }
    public string CS { get; set; }
    public string VM { get; set; }

    public int GetFileNameHashCode()
    {
        var hash = new HashCode();
        hash.Add(AXAML);
        hash.Add(CS);
        hash.Add(VM);
        return hash.ToHashCode();
    }
    public string LoadFile(string file)
    {
        try
        {
            return System.IO.File.ReadAllText(file);
        }
        catch (System.Exception ex)
        {
            return ex.Message;
        }
    }
    public CodeDialogViewModel()
    {
        AXAML = "F:\\Program\\VS2022SW\\WPF\\WPFSln\\AvaApp1\\Dialogs\\CodeDialog.axaml";
        CS = "F:\\Program\\VS2022SW\\WPF\\WPFSln\\AvaApp1\\Dialogs\\CodeDialog.axaml.cs";
        VM = "F:\\Program\\VS2022SW\\WPF\\WPFSln\\AvaApp1\\Dialogs\\DialogVMBase.cs";
    }

    public CodeDialogViewModel(string ViewName)
    {
        AXAML = $"src\\Views\\{ViewName}.axaml";
        CS = $"src\\Views\\{ViewName}.axaml.cs";
        if (ViewName.EndsWith("View"))
        {
            VM = $"src\\ViewModels\\{ViewName}Model.cs";
        }
        else
        {
            VM = $"src\\ViewModels\\{ViewName}ViewModel.cs";
        }

    }
}
