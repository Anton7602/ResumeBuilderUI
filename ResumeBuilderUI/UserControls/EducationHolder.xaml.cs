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
    /// Interaction logic for EducationHolder.xaml
    /// </summary>
    public partial class EducationHolder : UserControl
    {
        public EducationHolder()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty EducationProperty =
DependencyProperty.Register("EducationSource", typeof(Education), typeof(EducationHolder), new PropertyMetadata(OnEducationSourceChange));

        private static void OnEducationSourceChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EducationHolder).EducationDatesTextBox.Text = (d as EducationHolder).EducationSource.StartDate.ToString("Y", App.Language) +
                " - " + (d as EducationHolder).EducationSource.EndDate.ToString("Y", App.Language);
        }

        public Education EducationSource
        {
            get { return (Education)GetValue(EducationProperty); }
            set { SetValue(EducationProperty, value); }
        }
    }
}
