using ResumeBuilderUI.Models;
using ResumeBuilderUI.ViewModels;
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
    /// Interaction logic for ExperienceHolder.xaml
    /// </summary>
    public partial class ExperienceHolder : UserControl
    {
        private readonly Experience.Priorities[] _priorities = { Experience.Priorities.Low, Experience.Priorities.Medium, Experience.Priorities.High };
        public Experience.Priorities[] Priorities { get { return _priorities; } }

        #region Constructors
        public ExperienceHolder()
        {
            InitializeComponent();
        }
        #endregion

        #region UserControl Properies
        //ExperienceSource - Property for binding a Experience, shown in ExperienceHolder
        public static readonly DependencyProperty ExperienceProperty =
    DependencyProperty.Register("ExperienceSource", typeof(Experience), typeof(ExperienceHolder));
        public Experience ExperienceSource
        {
            get { return (Experience)GetValue(ExperienceProperty); }
            set { SetValue(ExperienceProperty, value); }
        }
        //ActiveMode - Property for defining curent viewmode of holder. Can be ShowMode or EditMode. Titles are self-explanatory
        public enum ViewMode { ShowMode, EditMode }
        public static readonly DependencyProperty ActiveModeProperty =
            DependencyProperty.Register("ActiveMode", typeof(ViewMode), typeof(ExperienceHolder), new PropertyMetadata(OnActiveModeChange));
        public ViewMode ActiveMode
        {
            get { return (ViewMode)GetValue(ActiveModeProperty); }
            set { SetValue(ActiveModeProperty, value); }
        }
        private static void OnActiveModeChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ExperienceHolder).SetUpViewMode((ViewMode)e.NewValue);
        }
        //AcceptCommand - Property to bind a ViewModel command, that triggers upon pressing accept button of a holder
        public static readonly DependencyProperty AcceptCommandProperty =
            DependencyProperty.Register("AcceptCommand", typeof(ICommand), typeof(ExperienceHolder));
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
                    TagTextBlock.Visibility = Visibility.Visible;
                    DescriptionTextBlock.Visibility = Visibility.Visible;
                    PriorityTextBlock.Visibility = Visibility.Visible;
                    TagTextField.Visibility = Visibility.Collapsed;
                    DescriptionTextField.Visibility = Visibility.Collapsed;
                    PriorityComboBox.Visibility = Visibility.Collapsed;
                    AcceptChangesButton.Visibility = Visibility.Collapsed;
                    CancelChangesButton.Visibility = Visibility.Collapsed;
                    break;
                case ViewMode.EditMode:
                    TagTextBlock.Visibility = Visibility.Collapsed;
                    DescriptionTextBlock.Visibility = Visibility.Collapsed;
                    PriorityTextBlock.Visibility = Visibility.Collapsed;
                    TagTextField.Visibility = Visibility.Visible;
                    DescriptionTextField.Visibility = Visibility.Visible;
                    PriorityComboBox.Visibility = Visibility.Visible;
                    AcceptChangesButton.Visibility = Visibility.Visible;
                    CancelChangesButton.Visibility = Visibility.Visible;
                    break;
            }
        }
        #endregion

        public event EventHandler AcceptChanges;
        private void AcceptChangesButton_Click(object sender, RoutedEventArgs e)
        {
            AcceptChanges.Invoke(this, EventArgs.Empty);
        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ActiveMode = ActiveMode.Equals(ViewMode.ShowMode) ? ViewMode.EditMode : ViewMode.ShowMode;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Experience experienceToRemove = new Experience();
            Employment employmetToRemoveFrom = new Employment();
            foreach(Employment employment in App.ActiveProfile.EmploymentsList)
            {
                foreach(Experience experience in employment.ExperiencesList)
                {
                    if(experience.Equals(ExperienceSource))
                    {
                        experienceToRemove = experience;
                        employmetToRemoveFrom = employment;
                        break;
                    }
                }
            }
            if (!experienceToRemove.Tag.Equals(string.Empty))
            {
                employmetToRemoveFrom.ExperiencesList.Remove(experienceToRemove);
            }
        }
    }
}
