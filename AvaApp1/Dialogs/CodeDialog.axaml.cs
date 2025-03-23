using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using AvaloniaEdit;
using AvaloniaEdit.TextMate;
using TextMateSharp.Grammars;
using Ursa.Controls;

namespace AvaApp1;

public partial class CodeDialog : UserControl
{
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
        var _registryOptions = new RegistryOptions(ThemeName.DarkPlus);
        var html = _registryOptions.GetScopeByLanguageId("html");
        var csharp = _registryOptions.GetScopeByLanguageId("csharp");

        edit_CS.Text = vm.LoadFile(vm.CS);
        edit_AXAML.Text = vm.LoadFile(vm.AXAML);
        edit_VM.Text = vm.LoadFile(vm.VM);
        var _textMateCS = edit_CS.InstallTextMate(_registryOptions);
        //😀, you are ready to use AvaloniaEdit with syntax highlighting!
        _textMateCS.SetGrammar(csharp);
        var _textMateAXAML = edit_AXAML.InstallTextMate(_registryOptions);
        _textMateAXAML.SetGrammar(html);
        var _textMateVM = edit_VM.InstallTextMate(_registryOptions);
        _textMateVM.SetGrammar(csharp);
    }
}
