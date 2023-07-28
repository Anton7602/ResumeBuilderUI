using ResumeBuilderUI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResumeBuilderUI.UserControls
{
    public partial class SkillHolder : UserControl
    {
        public SkillHolder()
        {
            InitializeComponent();
            ElementsToggleButtons = new ObservableCollection<ToggleButton>();
            collectionContainer.Collection = ElementsToggleButtons;
            compositeCollection.Add(AddNewSkillButton);
            compositeCollection.Add(collectionContainer);
            SkillHolderListBox.ItemsSource = compositeCollection;
        }

        private void SkillsList_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if ((sender as ObservableCollection<string>).Equals(SkillsSource))
            {
                ToggleButton tempToggleButton;
                foreach (string skill in e.NewItems)
                {
                    tempToggleButton = new ToggleButton();
                    tempToggleButton.Content = skill;
                    tempToggleButton.Style = App.Current.Resources["SkillElementsStyle"] as Style;
                    ElementsToggleButtons.Insert(0, tempToggleButton);
                }
                collectionContainer.Collection = ElementsToggleButtons;
            }
        }

        private readonly CompositeCollection compositeCollection = new CompositeCollection();
        private readonly CollectionContainer collectionContainer = new CollectionContainer();
        private TextFieldButton AddNewSkillButton = new TextFieldButton();


        public ObservableCollection<ToggleButton> ElementsToggleButtons { get; set; }

        public ObservableCollection<string> SkillsSource
        {
            get { return (ObservableCollection<string>)GetValue(SkillsSourceProperty); }
            set 
            { 
                SetValue(SkillsSourceProperty, value);
            }
        }

        public static readonly DependencyProperty SkillsSourceProperty =
            DependencyProperty.Register("SkillsSource", typeof(ObservableCollection<string>), typeof(SkillHolder), new PropertyMetadata(OnSkillsSourceChanged));

        private static void OnSkillsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ToggleButton tempToggleButton;
            (d as SkillHolder).SkillsSource.CollectionChanged += (d as SkillHolder).SkillsList_CollectionChanged;
            foreach (string skill in (d as SkillHolder).SkillsSource)
            {
                tempToggleButton = new ToggleButton();
                tempToggleButton.Content= skill;
                tempToggleButton.Style = App.Current.Resources["SkillElementsStyle"] as Style;
                (d as SkillHolder).ElementsToggleButtons.Add(tempToggleButton);
            }
            (d as SkillHolder).collectionContainer.Collection = (d as SkillHolder).ElementsToggleButtons;
        }

        public static readonly DependencyProperty AcceptCommandProperty =
    DependencyProperty.Register("AcceptCommand", typeof(ICommand), typeof(SkillHolder), new PropertyMetadata(OnAcceptCommandChange));

        private static void OnAcceptCommandChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SkillHolder).AddNewSkillButton.AcceptCommand = (d as SkillHolder).AcceptCommand;
        }

        public ICommand AcceptCommand
        {
            get { return (ICommand)GetValue(AcceptCommandProperty); }
            set { SetValue(AcceptCommandProperty, value); }
        }
    }
}
