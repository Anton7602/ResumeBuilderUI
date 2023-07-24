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
            App.activeProfile.SkillsetsList[0].SkillsList.CollectionChanged += SkillsList_CollectionChanged;
            Toggles = new ObservableCollection<ToggleButton>();
            collectionContainer.Collection = Toggles;
            Button AddNewSkillButton = new Button();
            AddNewSkillButton.Content = "+";
            AddNewSkillButton.Style = App.Current.Resources["AddSkillButtonStyle"] as Style;
            AddNewSkillButton.Click += AddNewSkillButton_Click;
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
                    Toggles.Insert(0, tempToggleButton);
                }
                collectionContainer.Collection = Toggles;
            }
        }

        private void AddNewSkillButton_Click(object sender, RoutedEventArgs e)
        {
            App.activeProfile.SkillsetsList[0].SkillsList.Insert(0,"TestSkill");
        }

        private readonly CompositeCollection compositeCollection = new CompositeCollection();
        private readonly CollectionContainer collectionContainer = new CollectionContainer();


        public ObservableCollection<ToggleButton> Toggles { get; set; }

        public System.Collections.IEnumerable SkillsSource
        {
            get { return (IEnumerable)GetValue(SkillsSourceProperty); }
            set 
            { 
                SetValue(SkillsSourceProperty, value);
            }
        }

        public static readonly DependencyProperty SkillsSourceProperty =
            DependencyProperty.Register("SkillsSource", typeof(IEnumerable), typeof(SkillHolder), new PropertyMetadata(OnSkillsSourceChanged));

        private static void OnSkillsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //(d as SkillHolder).collectionContainer.Collection = (d as SkillHolder).SkillsSource;
            ToggleButton tempToggleButton;
            foreach(string skill in (d as SkillHolder).SkillsSource)
            {
                tempToggleButton = new ToggleButton();
                tempToggleButton.Content= skill;
                tempToggleButton.Style = App.Current.Resources["SkillElementsStyle"] as Style;
                (d as SkillHolder).Toggles.Add(tempToggleButton);
            }
            (d as SkillHolder).collectionContainer.Collection = (d as SkillHolder).Toggles;
        }
    }
}
