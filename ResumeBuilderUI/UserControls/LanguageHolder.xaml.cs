using ResumeBuilderUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
