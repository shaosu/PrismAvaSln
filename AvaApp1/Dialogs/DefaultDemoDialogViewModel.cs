using System.Collections.ObjectModel;

namespace AvaApp1;

public partial class DefaultDemoDialogViewModel : DialogVMBase
{
    public ObservableCollection<string> Cities { get; set; }
    public DefaultDemoDialogViewModel()
    {
        Cities =
        [
            "Shanghai", "Beijing", "Hulunbuir", "Shenzhen", "Hangzhou", "Nanjing", "Chengdu", "Wuhan", "Chongqing",
            "Suzhou", "Tianjin", "Xi'an", "Qingdao", "Dalian"
        ];
    }
}
