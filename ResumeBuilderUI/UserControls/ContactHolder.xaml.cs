using ResumeBuilderUI.Models;
using System.Windows;
using System.Windows.Controls;

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
