using MD5TestApplication.ViewModel;
using System.Windows;

namespace MD5TestApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MD5TestApplicationViewModel();
        }
    }
}
