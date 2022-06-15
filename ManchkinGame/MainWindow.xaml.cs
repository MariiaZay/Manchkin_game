using System.Windows;

namespace ManchkinGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowLogic _logic;
        public MainWindow()
        {
            InitializeComponent();
            _logic = new MainWindowLogic(this);
        }
    }
}