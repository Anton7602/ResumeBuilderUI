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
    /// Interaction logic for ContactHolder.xaml
    /// </summary>
    public partial class ContactHolder : UserControl
    {
        public ContactHolder()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ContactProperty =
DependencyProperty.Register("ContactSource", typeof(Contact), typeof(ContactHolder));

        public Contact ContactSource
        {
            get { return (Contact)GetValue(ContactProperty); }
            set { SetValue(ContactProperty, value); }
        }
    }
}
