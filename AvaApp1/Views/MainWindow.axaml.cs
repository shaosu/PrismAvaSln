using AvaApp1.ViewModels;
using Avalonia.Controls;
using Ursa.Controls;

namespace AvaApp1.Views
{
    /// <summary>Main window view.</summary>
    public partial class MainWindow : UrsaWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.SizeChanged += MainWindow_SizeChanged;
        }

        private void MainWindow_SizeChanged(object? sender, SizeChangedEventArgs e)
        {
            MainWindowViewModel vm = (MainWindowViewModel)this.DataContext;
            vm.Width2 = e.NewSize.Width - 200;
        }
    }
}
