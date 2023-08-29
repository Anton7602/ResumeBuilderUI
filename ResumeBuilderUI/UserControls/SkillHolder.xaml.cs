using ResumeBuilderUI.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ResumeBuilderUI.UserControls
{
    public partial class SkillHolder : UserControl
    {
        #region Constructors
        public SkillHolder()
        {
            InitializeComponent();
            ElementsToggleButtons = new ObservableCollection<ToggleButton>();
            collectionContainer.Collection = ElementsToggleButtons;
            compositeCollection.Add(AddNewSkillButton);
            compositeCollection.Add(collectionContainer);
            SkillHolderListBox.ItemsSource = compositeCollection;
        }
        #endregion

        #region Fields and Properties
        private readonly CompositeCollection compositeCollection = new CompositeCollection();
        private readonly CollectionContainer collectionContainer = new CollectionContainer();
        private TextFieldButton AddNewSkillButton = new TextFieldButton();
        public ObservableCollection<ToggleButton> ElementsToggleButtons { get; set; }

        #endregion

        #region UserControl Properties
        //SkillSource - Property for binding a Skills Collection, shown in SkillHolder
        public static readonly DependencyProperty SkillsSourceProperty =
            DependencyProperty.Register("SkillsSource", typeof(ObservableCollection<Skill>), typeof(SkillHolder), new PropertyMetadata(OnSkillsSourceChanged));
        public ObservableCollection<Skill> SkillsSource
        {
            get { return (ObservableCollection<Skill>)GetValue(SkillsSourceProperty); }
            set
            {
                SetValue(SkillsSourceProperty, value);
            }
        }
        private static void OnSkillsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SkillHolder).SkillsSource.CollectionChanged += (d as SkillHolder).SkillsList_CollectionChanged;
            foreach(Skill skill in (d as SkillHolder).SkillsSource)
            {
                (d as SkillHolder).ElementsToggleButtons.Add((d as SkillHolder).GenerateSkillToggleButton(skill));
            }
            (d as SkillHolder).collectionContainer.Collection = (d as SkillHolder).ElementsToggleButtons;
        }
        /// <summary>
        /// Listener for SkillList ObservableCollection changes. If triggered - finds appropriate skillset, wraps skill in ToggleButton and 
        /// adds to a SkillHolder.
        /// </summary>
        private void SkillsList_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if ((sender as ObservableCollection<Skill>).Equals(SkillsSource))
            {
                if (e.NewItems != null)
                {
                    foreach (Skill skill in e.NewItems)
                    {
                        ElementsToggleButtons.Insert(0, GenerateSkillToggleButton(skill));
                    }
                    collectionContainer.Collection = ElementsToggleButtons;
                }
                if (e.OldItems != null )
                {
                    foreach (Skill skill in e.OldItems)
                    {
                        ToggleButton toggleButtonToRemove = new ToggleButton();
                        foreach (ToggleButton toggleButton in ElementsToggleButtons)
                        {
                            if(toggleButton.Content.Equals(skill.SkillName))
                            {
                                toggleButtonToRemove = toggleButton;
                                break;
                            }
                        }
                        ElementsToggleButtons.Remove(toggleButtonToRemove);
                    }
                    collectionContainer.Collection = ElementsToggleButtons;
                }
            }
        }
        //AcceptCommand - Property to bind a ViewModel command, that triggers upon pressing accept button of a holder
        public static readonly DependencyProperty AcceptCommandProperty =
    DependencyProperty.Register("AcceptCommand", typeof(ICommand), typeof(SkillHolder), new PropertyMetadata(OnAcceptCommandChange));
        public ICommand AcceptCommand
        {
            get { return (ICommand)GetValue(AcceptCommandProperty); }
            set { SetValue(AcceptCommandProperty, value); }
        }
        private static void OnAcceptCommandChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SkillHolder).AddNewSkillButton.AcceptCommand = (d as SkillHolder).AcceptCommand;
        }
        #endregion

        #region Private Methods
        private ToggleButton GenerateSkillToggleButton(Skill skill)
        {
            ToggleButton tempToggleButton = new ToggleButton();
            Binding binding = new Binding();
            binding.Source = skill;
            binding.Path = new PropertyPath("IsSelected");
            tempToggleButton.Content = skill.SkillName;
            tempToggleButton.Style = App.Current.Resources["ListElementsStyle"] as Style;
            tempToggleButton.SetBinding(ToggleButton.IsCheckedProperty, binding);
            tempToggleButton.Foreground = Brushes.Red;
            tempToggleButton.MouseDoubleClick += ToggleButton_MouseDoubleClick;
            //Generating Tooltip
            string tooltip = string.Empty;
            int counter = 0;
            foreach (Employment employment in App.ActiveProfile.EmploymentsList)
            {
                foreach (Experience experience in employment.ExperiencesList)
                {
                    if (skill.SkillName.Equals(experience.Tag) && employment.IsSelected)
                    {
                        tempToggleButton.Foreground = App.Current.Resources["PrimaryTextColor"] as Brush;
                        tooltip += employment.Employer + " " + employment.Title + "\n";
                        counter++;
                    }
                }
            }
            switch(counter)
            {
                case 0:
                    tempToggleButton.ToolTip = App.Current.Resources["lang_Skills_Tooltip_None"] as string;
                    break;
                case 1:
                    tempToggleButton.ToolTip = (App.Current.Resources["lang_Skills_Tooltip_Singular"] as string) + "\n" + tooltip;
                    break;
                default:
                    tempToggleButton.ToolTip = (App.Current.Resources["lang_Skills_Tooltip_Multiple_Start"] as string) + " " + counter+ " " +
                        (App.Current.Resources["lang_Skills_Tooltip_Multiple_End"] as string) + "\n" + tooltip;
                    break;
            }
            return tempToggleButton;
        }

        private void ToggleButton_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Skill skillToRemove = new Skill(string.Empty);
            foreach (Skillset skillset in App.ActiveProfile.SkillsetsList)
            {
                foreach (Skill skill in skillset.SkillsList)
                {
                    if (skill.SkillName.Equals((sender as ToggleButton).Content))
                    {
                        skillToRemove = skill;
                        break;
                    }
                }
                if (!skillToRemove.SkillName.Equals(string.Empty))
                {
                    skillset.SkillsList.Remove(skillToRemove);
                    break;
                }
            }
        }
        #endregion
    }
}
