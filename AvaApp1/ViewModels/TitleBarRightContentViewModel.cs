using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaApp1.Events;
using Prism.Events;
using Prism.Mvvm;

namespace AvaApp1.ViewModels;

public class TitleBarRightContentViewModel : BindableBase
{
    IEventAggregator _ea;
    public CodeDialogViewModel? _codeModel;

    public string? Title { get; set; }

    public TitleBarRightContentViewModel(Prism.Events.IEventAggregator ea)
    {
        _ea = ea;
        _ea.GetEvent<CodeFileChangedEvent>().Subscribe(OnCodeFileChanged);
        Title = "TitleBarRightContentViewModel.txt";
    }

    private void OnCodeFileChanged(CodeDialogViewModel obj)
    {
        _codeModel = obj;
    }
}
