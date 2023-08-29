using ResumeBuilderUI.Models;
using ResumeBuilderUI.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ResumeBuilderUI.UserControls
{
    /// <summary>
    /// Interaction logic for EmploymentHolder.xaml
    /// </summary>
    public partial class EmploymentHolder : UserControl
    {
        #region Field and Properties
        private Experience _editedExperience = new Experience();
        public Experience EditedExperience
        {
            get { return _editedExperience; }
            set
            {
                _editedExperience = value;
            }
        }
        #endregion

        #region Constructors
        public EmploymentHolder()
        {
            InitializeComponent();
            EditedExperienceHolder.AcceptChanges += ExperienceHolderAcceptChanges_Click;
        }

        private void ExperienceHolderAcceptChanges_Click(object? sender, EventArgs e)
        {
            EmploymentSource.ExperiencesList.Insert(0, new Experience(EditedExperience));
            EditedExperience.Tag = string.Empty;
            EditedExperience.Description = string.Empty;
            EditedExperienceHolder.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region UserControl Properies
        //EmploymentSource - Property for binding a Employment, shown in EmploymentHolder
        public static readonly DependencyProperty EmploymentProperty =
    DependencyProperty.Register("EmploymentSource", typeof(Employment), typeof(EmploymentHolder), new PropertyMetadata(OnEmploymentSourceChanged));
        public Employment EmploymentSource
        {
            get { return (Employment)GetValue(EmploymentProperty); }
            set { SetValue(EmploymentProperty, value); }
        }
        private static void OnEmploymentSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EmploymentHolder).EmploymentDatesTextBlock.Text = (d as EmploymentHolder).EmploymentSource.StartDate.ToString("Y", App.Language)
                + "-" + (d as EmploymentHolder).EmploymentSource.EndDate.ToString("Y", App.Language);
        }
        //ActiveMode - Property for defining curent viewmode of holder. Can be ShowMode or EditMode. Titles are self-explanatory
        public enum ViewMode { ShowMode, EditMode }
        public static readonly DependencyProperty ActiveModeProperty =
            DependencyProperty.Register("ActiveMode", typeof(ViewMode), typeof(EmploymentHolder), new PropertyMetadata(OnActiveModeChange));
        public ViewMode ActiveMode
        {
            get { return (ViewMode)GetValue(ActiveModeProperty); }
            set { SetValue(ActiveModeProperty, value); }
        }
        private static void OnActiveModeChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EmploymentHolder).SetUpViewMode((ViewMode)e.NewValue);
        }
        //AcceptCommand - Property to bind a ViewModel command, that triggers upon pressing accept button of a holder
        public static readonly DependencyProperty AcceptCommandProperty =
            DependencyProperty.Register("AcceptCommand", typeof(ICommand), typeof(EmploymentHolder));
        public ICommand AcceptCommand
        {
            get { return (ICommand)GetValue(AcceptCommandProperty); }
            set { SetValue(AcceptCommandProperty, value); }
        }
        #endregion

        #region Private Methods
        private void SetUpViewMode(ViewMode mode)
        {
            switch (mode)
            {
                case ViewMode.ShowMode:
                    EmploymentHolderToggle.Style = App.Current.Resources["ListElementsStyle"] as Style;
                    EmployerTextBlock.Visibility = Visibility.Visible;
                    TitleTextBlock.Visibility = Visibility.Visible;
                    EmploymentDatesTextBlock.Visibility = Visibility.Visible;
                    ExperiencesToggleButton.IsChecked = false;
                    ExperiencesToggleButton.Visibility = Visibility.Visible;
                    EmployerTextField.Visibility = Visibility.Collapsed;
                    TitleTextField.Visibility = Visibility.Collapsed;
                    AcceptChangesButton.Visibility= Visibility.Collapsed;
                    CancelChangesButton.Visibility= Visibility.Collapsed;
                    break;
                case ViewMode.EditMode:
                    EmploymentHolderToggle.Style = App.Current.Resources["EditedListElementsStyle"] as Style;
                    EmployerTextBlock.Visibility = Visibility.Collapsed;
                    TitleTextBlock.Visibility = Visibility.Collapsed;
                    EmploymentDatesTextBlock.Visibility = Visibility.Collapsed;
                    ExperiencesToggleButton.IsChecked = false;
                    ExperiencesToggleButton.Visibility = Visibility.Collapsed;
                    EmployerTextField.Visibility = Visibility.Visible;
                    TitleTextField.Visibility = Visibility.Visible;
                    AcceptChangesButton.Visibility = Visibility.Visible;
                    CancelChangesButton.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void AddNewExperienceButton_Click(object sender, RoutedEventArgs e)
        {
            EditedExperienceHolder.Visibility = Visibility.Visible;
            EditedExperienceHolder.TagTextField.Focus();
        }
        #endregion

        private void EmploymentHolderToggle_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ActiveMode = ActiveMode.Equals(ViewMode.ShowMode) ? ViewMode.EditMode : ViewMode.ShowMode;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            App.ActiveProfile.EmploymentsList.Remove(EmploymentSource);
        }
    }
}
