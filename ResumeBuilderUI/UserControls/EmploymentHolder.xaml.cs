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
    /// Interaction logic for EmploymentHolder.xaml
    /// </summary>
    public partial class EmploymentHolder : UserControl
    {
        public EmploymentHolder()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty EmploymentProperty =
    DependencyProperty.Register("EmploymentSource", typeof(Employment), typeof(EmploymentHolder), new PropertyMetadata(OnEmploymentSourceChanged));

        private static void OnEmploymentSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EmploymentHolder).EmploymentDatesTextBox.Text = (d as EmploymentHolder).EmploymentSource.StartDate.ToString("Y", App.Language)
                + "-" + (d as EmploymentHolder).EmploymentSource.EndDate.ToString("Y", App.Language);
        }

        public Employment EmploymentSource
        {
            get { return (Employment)GetValue(EmploymentProperty); }
            set { SetValue(EmploymentProperty, value); }
        }
    }
}
