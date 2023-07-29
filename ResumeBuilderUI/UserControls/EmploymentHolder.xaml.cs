using ResumeBuilderUI.Models;
using System.Windows;
using System.Windows.Controls;

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
