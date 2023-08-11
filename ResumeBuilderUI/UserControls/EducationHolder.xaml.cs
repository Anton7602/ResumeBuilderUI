using ResumeBuilderUI.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ResumeBuilderUI.UserControls
{
    /// <summary>
    /// Interaction logic for EducationHolder.xaml
    /// </summary>
    public partial class EducationHolder : UserControl
    {
        #region Constructors
        public EducationHolder()
        {
            InitializeComponent();
        }
        #endregion

        #region UserControl Properties
        //EducationSource - Property for binding a Education, shown in EducationHolder
        public static readonly DependencyProperty EducationProperty =
            DependencyProperty.Register("EducationSource", typeof(Education), typeof(EducationHolder), new PropertyMetadata(OnEducationSourceChange));
        public Education EducationSource
        {
            get { return (Education)GetValue(EducationProperty); }
            set { SetValue(EducationProperty, value); }
        }
        private static void OnEducationSourceChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EducationHolder).EducationDatesTextBox.Text = (d as EducationHolder).EducationSource.StartDate.ToString("Y", App.Language) +
                " - " + (d as EducationHolder).EducationSource.EndDate.ToString("Y", App.Language);
        }
        //ActiveMode - Property for defining curent viewmode of holder. Can be ShowMode or EditMode. Titles are self-explanatory
        public enum ViewMode { ShowMode, EditMode }
        public static readonly DependencyProperty ActiveModeProperty =
            DependencyProperty.Register("ActiveMode", typeof(ViewMode), typeof(EducationHolder), new PropertyMetadata(OnActiveModeChange));
        public ViewMode ActiveMode
        {
            get { return (ViewMode)GetValue(ActiveModeProperty); }
            set { SetValue(ActiveModeProperty, value); }
        }
        private static void OnActiveModeChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EducationHolder).SetUpViewMode((ViewMode)e.NewValue);
        }
        //AcceptCommand - Property to bind a ViewModel command, that triggers upon pressing accept button of a holder
        public static readonly DependencyProperty AcceptCommandProperty =
            DependencyProperty.Register("AcceptCommand", typeof(ICommand), typeof(EducationHolder));
        public ICommand AcceptCommand
        {
            get { return (ICommand)GetValue(AcceptCommandProperty); }
            set { SetValue(AcceptCommandProperty, value); }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Switches visibility parameters of the holder's elements according to picked ViewMode
        /// </summary>
        private void SetUpViewMode(ViewMode mode)
        {
            switch (mode)
            {
                case ViewMode.ShowMode:
                    EducationHolderToggle.Style = App.Current.Resources["ListElementsStyle"] as Style;
                    InstitutionTextBlock.Visibility = Visibility.Visible;
                    ProgramTextBlock.Visibility = Visibility.Visible;
                    DescriptionTextBlock.Visibility = Visibility.Visible;
                    DegreeTextBlock.Visibility = Visibility.Visible;
                    EducationDatesTextBox.Visibility = Visibility.Visible;
                    InstitutionTextField.Visibility = Visibility.Collapsed;
                    ProgramTextField.Visibility = Visibility.Collapsed;
                    DescriptionTextField.Visibility = Visibility.Collapsed;
                    DegreeTextField.Visibility = Visibility.Collapsed;
                    AcceptChangesButton.Visibility = Visibility.Collapsed;
                    CancelChangesButton.Visibility = Visibility.Collapsed;
                    break;
                case ViewMode.EditMode:
                    EducationHolderToggle.Style = App.Current.Resources["EditedListElementsStyle"] as Style;
                    InstitutionTextBlock.Visibility = Visibility.Collapsed;
                    ProgramTextBlock.Visibility = Visibility.Collapsed;
                    DescriptionTextBlock.Visibility = Visibility.Collapsed;
                    DegreeTextBlock.Visibility = Visibility.Collapsed;
                    EducationDatesTextBox.Visibility = Visibility.Collapsed;
                    InstitutionTextField.Visibility = Visibility.Visible;
                    ProgramTextField.Visibility = Visibility.Visible;
                    DescriptionTextField.Visibility = Visibility.Visible;
                    DegreeTextField.Visibility = Visibility.Visible;
                    AcceptChangesButton.Visibility = Visibility.Visible;
                    CancelChangesButton.Visibility = Visibility.Visible;
                    break;
            }
            #endregion
        }
    }
}
