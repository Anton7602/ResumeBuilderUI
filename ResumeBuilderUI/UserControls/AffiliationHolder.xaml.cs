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
        public AffiliationHolder()
        {
            InitializeComponent();
            SetUpViewMode(ActiveMode);
        }


        public static readonly DependencyProperty AffiliationProperty =
    DependencyProperty.Register("AffiliationSource", typeof(ProffessionalAffiliation), typeof(AffiliationHolder));

        public ProffessionalAffiliation AffiliationSource
        {
            get { return (ProffessionalAffiliation)GetValue(AffiliationProperty); }
            set { SetValue(AffiliationProperty, value); }
        }

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

        public static readonly DependencyProperty AcceptCommandProperty =
DependencyProperty.Register("AcceptCommand", typeof(ICommand), typeof(AffiliationHolder));

        public ICommand AcceptCommand
        {
            get { return (ICommand)GetValue(AcceptCommandProperty); }
            set { SetValue(AcceptCommandProperty, value); }
        }
    }
}
