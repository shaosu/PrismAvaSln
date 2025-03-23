namespace AvaApp1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainViewViewModel MainViewViewModel { get; set; } = new MainViewViewModel();
        public MainWindowViewModel()
        {
            Title = "Prism.Avalonia";
        }


        private double _Width;
        public double Width
        {
            get { return _Width; }
            set
            {
                SetProperty(ref _Width, value);
                Width2 = value - 900;
            }
        }


        private double _Width2;
        public double Width2
        {
            get { return _Width2; }
            set
            {
                SetProperty(ref _Width2, value);
            }
        }

    }
}
