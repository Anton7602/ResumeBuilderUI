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
            collectionContainer.Collection = SkillsSource;
            Button AddNewSkillButton = new Button();
            AddNewSkillButton.Content = "+";
            AddNewSkillButton.Click += AddNewSkillButton_Click;
            compositeCollection.Add(AddNewSkillButton);
            compositeCollection.Add(collectionContainer);
            SkillHolderListBox.ItemsSource = compositeCollection;
        }

        private void AddNewSkillButton_Click(object sender, RoutedEventArgs e)
        {
            App.activeProfile.SkillsetsList[0].SkillsList.Insert(0,"TestSkill");
        }

        private readonly CompositeCollection compositeCollection = new CompositeCollection();
        private readonly CollectionContainer collectionContainer = new CollectionContainer();


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
            (d as SkillHolder).collectionContainer.Collection = (d as SkillHolder).SkillsSource;
        }
    }
}
