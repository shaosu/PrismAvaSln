namespace AvaApp1;

public class CodeDialogViewModel : DialogVMBase
{
    public string AXAML { get; set; }
    public string CS { get; set; }
    public string VM { get; set; }

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
}
