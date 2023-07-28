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
