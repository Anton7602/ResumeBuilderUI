using ResumeBuilderUI.Models;
using System.Windows;
using System.Windows.Controls;

namespace ResumeBuilderUI.UserControls
{
    /// <summary>
    /// Interaction logic for LanguageHolder.xaml
    /// </summary>
    public partial class LanguageHolder : UserControl
    {
        public LanguageHolder()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty LanguageProperty =
DependencyProperty.Register("LanguageSource", typeof(Language), typeof(LanguageHolder));

        public Language LanguageSource
        {
            get { return (Language)GetValue(LanguageProperty); }
            set { SetValue(LanguageProperty, value); }
        }
    }
}
