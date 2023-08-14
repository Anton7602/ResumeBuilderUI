using ResumeBuilderUI.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ResumeBuilderUI.UserControls
{
    /// <summary>
    /// Interaction logic for AffiliationHolder.xaml
    /// </summary>
    public partial class AffiliationHolder : UserControl
    {
        #region Constructors
        public AffiliationHolder()
        {
            InitializeComponent();
            SetUpViewMode(ActiveMode);
        }
        #endregion

        #region UserControlProperties
        //AffiliationSource - Property for binding an Affiliation, shown in AffiliationHolder
        public static readonly DependencyProperty AffiliationProperty =
            DependencyProperty.Register("AffiliationSource", typeof(ProffessionalAffiliation), typeof(AffiliationHolder));
        public ProffessionalAffiliation AffiliationSource
        {
            get { return (ProffessionalAffiliation)GetValue(AffiliationProperty); }
            set { SetValue(AffiliationProperty, value); }
        }
        //ActiveMode - Property for defining curent viewmode of holder. Can be ShowMode or EditMode. Titles are self-explanatory
        public enum ViewMode {ShowMode, EditMode}
        public static readonly DependencyProperty ActiveModeProperty =
            DependencyProperty.Register("ActiveMode", typeof(ViewMode), typeof(AffiliationHolder), new PropertyMetadata(OnActiveModeChange));

        public ViewMode ActiveMode
        {
            get { return (ViewMode)GetValue(ActiveModeProperty); }
            set { SetValue(ActiveModeProperty, value); }
        }
        private static void OnActiveModeChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AffiliationHolder).SetUpViewMode((ViewMode)e.NewValue);
        }
        //AcceptCommand - Property to bind a ViewModel command, that triggers upon pressing accept button of a holder
        public static readonly DependencyProperty AcceptCommandProperty =
            DependencyProperty.Register("AcceptCommand", typeof(ICommand), typeof(AffiliationHolder));

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
                    AffiliationHolderToggle.Style = App.Current.Resources["ListElementsStyle"] as Style;
                    DateTextBlock.Visibility = Visibility.Visible;
                    CompanyTextBlock.Visibility = Visibility.Visible;
                    DescriptionTextBlock.Visibility = Visibility.Visible;
                    DateTextBox.Visibility = Visibility.Collapsed;
                    CompanyTextField.Visibility = Visibility.Collapsed;
                    DescriptionTextField.Visibility = Visibility.Collapsed;
                    AcceptChangesButton.Visibility = Visibility.Collapsed;
                    CancelChangesButton.Visibility = Visibility.Collapsed;
                    break;
                case ViewMode.EditMode:
                    AffiliationHolderToggle.Style = App.Current.Resources["EditedListElementsStyle"] as Style;
                    DateTextBlock.Visibility = Visibility.Collapsed;
                    CompanyTextBlock.Visibility = Visibility.Collapsed;
                    DescriptionTextBlock.Visibility = Visibility.Collapsed;
                    DateTextBox.Visibility = Visibility.Visible;
                    CompanyTextField.Visibility = Visibility.Visible;
                    DescriptionTextField.Visibility = Visibility.Visible;
                    AcceptChangesButton.Visibility = Visibility.Visible;
                    CancelChangesButton.Visibility = Visibility.Visible;
                    CompanyTextField.Focus();
                    break;
                default:
                    break;
            }
        }

        private void CancelChangesButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        private void AffiliationHolderToggle_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ActiveMode = ActiveMode.Equals(ViewMode.ShowMode) ? ViewMode.EditMode : ViewMode.ShowMode;
        }
    }
}
