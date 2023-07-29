using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace ResumeBuilderUI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.Language = new CultureInfo(ResumeBuilderUI.Properties.Settings.Default.DefaultLanguage);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
