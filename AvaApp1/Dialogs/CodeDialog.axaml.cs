using Avalonia;
using Avalonia.Controls;
using AvaloniaEdit.TextMate;
using TextMateSharp.Grammars;

namespace AvaApp1;

public partial class CodeDialog : UserControl
{
    private static int FileNameHashCode;
    private static CodeDialogViewModel? Cache;

    private CodeDialogViewModel? _viewModel;
    public CodeDialog()
    {
        InitializeComponent();
        this.SizeChanged += CodeDialog_SizeChanged;
    }

    private void CodeDialog_SizeChanged(object? sender, SizeChangedEventArgs e)
    {
        // Debug.WriteLine($"Width:{e.NewSize.Width} Height:{e.NewSize.Height}");
        if (e.WidthChanged)
        {
            if (e.NewSize.Width < e.PreviousSize.Width)
            {
                this.Width = e.PreviousSize.Width;
            }
        }
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _viewModel = this.DataContext as CodeDialogViewModel;
        if (_viewModel is not null)
        {
            SetGrammar(_viewModel);
        }
    }

    void SetGrammar(CodeDialogViewModel vm)
    {
        int hash = vm.GetFileNameHashCode();
        if (FileNameHashCode == hash && Cache != null)
        {
            SetGrammar_Cache(Cache);
        }
        FileNameHashCode = hash;
        Cache = new CodeDialogViewModel();
        Cache.CS = vm.LoadFile(vm.CS);
        Cache.AXAML = vm.LoadFile(vm.AXAML);
        Cache.VM = vm.LoadFile(vm.VM);
        SetGrammar_Cache(Cache);
    }

    private void SetGrammar_Cache(CodeDialogViewModel Cache)
    {
        var _registryOptions = new RegistryOptions(ThemeName.DarkPlus);
        var html = _registryOptions.GetScopeByLanguageId("html");
        var csharp = _registryOptions.GetScopeByLanguageId("csharp");
        edit_CS.Text = Cache.CS;
        edit_AXAML.Text = Cache.AXAML;
        edit_VM.Text = Cache.VM;
        var _textMateCS = edit_CS.InstallTextMate(_registryOptions);
        //😀, you are ready to use AvaloniaEdit with syntax highlighting!
        _textMateCS.SetGrammar(csharp);
        var _textMateAXAML = edit_AXAML.InstallTextMate(_registryOptions);
        _textMateAXAML.SetGrammar(html);
        var _textMateVM = edit_VM.InstallTextMate(_registryOptions);
        _textMateVM.SetGrammar(csharp);
    }
}
