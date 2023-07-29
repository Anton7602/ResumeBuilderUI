using ResumeBuilderUI.Models;
using System.Windows;
using System.Windows.Controls;

namespace ResumeBuilderUI.UserControls
{
    /// <summary>
    /// Interaction logic for AffiliationHolder.xaml
    /// </summary>
    public partial class AffiliationHolder : UserControl
    {
        public AffiliationHolder()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty AffiliationProperty =
    DependencyProperty.Register("AffiliationSource", typeof(ProffessionalAffiliation), typeof(AffiliationHolder));

        public ProffessionalAffiliation AffiliationSource
        {
            get { return (ProffessionalAffiliation)GetValue(AffiliationProperty); }
            set { SetValue(AffiliationProperty, value); }
        }
    }
}
