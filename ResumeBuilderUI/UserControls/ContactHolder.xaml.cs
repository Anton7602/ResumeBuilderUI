using ResumeBuilderUI.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public static readonly DependencyProperty AcceptCommandProperty =
DependencyProperty.Register("AcceptCommand", typeof(ICommand), typeof(ContactHolder));

        public ICommand AcceptCommand
        {
            get { return (ICommand)GetValue(AcceptCommandProperty); }
            set { SetValue(AcceptCommandProperty, value); }
        }

        public enum ViewMode { ShowMode, EditMode }
        public static readonly DependencyProperty ActiveModeProperty =
DependencyProperty.Register("ActiveMode", typeof(ViewMode), typeof(ContactHolder), new PropertyMetadata(OnActiveModeChange));

        public ViewMode ActiveMode
        {
            get { return (ViewMode)GetValue(ActiveModeProperty); }
            set { SetValue(ActiveModeProperty, value); }
        }
        private static void OnActiveModeChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ContactHolder).SetUpViewMode((ViewMode)e.NewValue);
        }

        private void SetUpViewMode(ViewMode mode)
        {
            switch (mode)
            {
                case ViewMode.ShowMode:
                    ContactHolderToggle.Style = App.Current.Resources["ListElementsStyle"] as Style;
                    ContactTypeTextBlock.Visibility = Visibility.Visible;
                    ContactDescriptionTextBlock.Visibility = Visibility.Visible;
                    ContactTypeTextField.Visibility = Visibility.Collapsed;
                    ContactDescriptionTextField.Visibility = Visibility.Collapsed;
                    AcceptChangesButton.Visibility = Visibility.Collapsed;
                    CancelChangesButton.Visibility = Visibility.Collapsed;
                    break;
                case ViewMode.EditMode:
                    ContactHolderToggle.Style = App.Current.Resources["EditedListElementsStyle"] as Style;
                    ContactTypeTextBlock.Visibility = Visibility.Collapsed;
                    ContactDescriptionTextBlock.Visibility = Visibility.Collapsed;
                    ContactTypeTextField.Visibility = Visibility.Visible;
                    ContactDescriptionTextField.Visibility = Visibility.Visible;
                    AcceptChangesButton.Visibility = Visibility.Visible;
                    CancelChangesButton.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
    }
}
