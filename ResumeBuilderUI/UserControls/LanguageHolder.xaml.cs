using ResumeBuilderUI.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ResumeBuilderUI.UserControls
{
    /// <summary>
    /// Interaction logic for LanguageHolder.xaml
    /// </summary>
    public partial class LanguageHolder : UserControl
    {
        #region Constructors
        public LanguageHolder()
        {
            InitializeComponent();
        }
        #endregion

        #region UserControl Property
        //LanguageSource - Property for binding a Language, shown in LanguageHolder
        public static readonly DependencyProperty LanguageProperty =
            DependencyProperty.Register("LanguageSource", typeof(Language), typeof(LanguageHolder));
        public Language LanguageSource
        {
            get { return (Language)GetValue(LanguageProperty); }
            set { SetValue(LanguageProperty, value); }
        }
        //ActiveMode - Property for defining curent viewmode of holder. Can be ShowMode or EditMode. Titles are self-explanatory
        public enum ViewMode { ShowMode, EditMode }
        public static readonly DependencyProperty ActiveModeProperty =
            DependencyProperty.Register("ActiveMode", typeof(ViewMode), typeof(LanguageHolder), new PropertyMetadata(OnActiveModeChange));
        public ViewMode ActiveMode
        {
            get { return (ViewMode)GetValue(ActiveModeProperty); }
            set { SetValue(ActiveModeProperty, value); }
        }
        private static void OnActiveModeChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LanguageHolder).SetUpViewMode((ViewMode)e.NewValue);
        }
        //AcceptCommand - Property to bind a ViewModel command, that triggers upon pressing accept button of a holder
        public static readonly DependencyProperty AcceptCommandProperty =
            DependencyProperty.Register("AcceptCommand", typeof(ICommand), typeof(LanguageHolder));
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
                    LanguageHolderToggle.Style = App.Current.Resources["ListElementsStyle"] as Style;
                    LanguageNameTextBlock.Visibility = Visibility.Visible;
                    LanguageProfficiencyTextBlock.Visibility = Visibility.Visible;
                    LanguageNameTextField.Visibility = Visibility.Collapsed;
                    LanguageProficiencyTextField.Visibility = Visibility.Collapsed;
                    AcceptChangesButton.Visibility = Visibility.Collapsed;
                    CancelChangesButton.Visibility = Visibility.Collapsed;
                    break;
                case ViewMode.EditMode:
                    LanguageHolderToggle.Style = App.Current.Resources["EditedListElementsStyle"] as Style;
                    LanguageNameTextBlock.Visibility = Visibility.Collapsed;
                    LanguageProfficiencyTextBlock.Visibility = Visibility.Collapsed;
                    LanguageNameTextField.Visibility = Visibility.Visible;
                    LanguageProficiencyTextField.Visibility = Visibility.Visible;
                    AcceptChangesButton.Visibility = Visibility.Visible;
                    CancelChangesButton.Visibility = Visibility.Visible;
                    break;
            }
            #endregion
        }

        private void LanguageHolderToggle_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ActiveMode = ActiveMode.Equals(ViewMode.ShowMode) ? ViewMode.EditMode : ViewMode.ShowMode;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            App.ActiveProfile.LanguagesList.Remove(LanguageSource);
        }
    }
}
