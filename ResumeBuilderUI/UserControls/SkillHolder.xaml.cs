using ResumeBuilderUI.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

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
            ToggleButton tempToggleButton;
            Binding binding;
            (d as SkillHolder).SkillsSource.CollectionChanged += (d as SkillHolder).SkillsList_CollectionChanged;
            foreach(Skill skill in (d as SkillHolder).SkillsSource)
            {
                binding = new Binding();
                binding.Source = skill;
                binding.Path = new PropertyPath("IsSelected");
                tempToggleButton = new ToggleButton();
                tempToggleButton.Content= skill.SkillName;
                tempToggleButton.Style = App.Current.Resources["ListElementsStyle"] as Style;
                tempToggleButton.SetBinding(ToggleButton.IsCheckedProperty, binding);
                (d as SkillHolder).ElementsToggleButtons.Add(tempToggleButton);
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
                ToggleButton tempToggleButton;
                Binding binding;
                foreach (Skill skill in e.NewItems)
                {
                    binding = new Binding();
                    binding.Source = skill;
                    binding.Path = new PropertyPath("IsSelected");
                    tempToggleButton = new ToggleButton();
                    tempToggleButton.Content = skill.SkillName;
                    tempToggleButton.Style = App.Current.Resources["ListElementsStyle"] as Style;
                    tempToggleButton.SetBinding(ToggleButton.IsCheckedProperty, binding);
                    ElementsToggleButtons.Insert(0, tempToggleButton);
                }
                collectionContainer.Collection = ElementsToggleButtons;
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
    }
}
