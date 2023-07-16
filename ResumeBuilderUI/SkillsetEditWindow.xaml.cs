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
using System.Windows.Shapes;

namespace ResumeBuilderUI
{
    public partial class SkillsetEditWindow : Window
    {
        public Skillset editedSkillset;
        private BitmapImage addButtonImage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\resources\addButton.png"));
        private BitmapImage editButtonImage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\resources\editButton.png"));
        private BitmapImage removeButtonImage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\resources\removeButton.png"));
        public SkillsetEditWindow()
        {
            InitializeComponent();
            editedSkillset = new Skillset();
        }

        public SkillsetEditWindow(Skillset skillset)
        {
            InitializeComponent();
            editedSkillset = new Skillset(skillset);
            txtSkillset.Text = editedSkillset.MainSkill;
            GenerateSkillList();
        }

        public void GenerateSkillList()
        {
            stckpnlSkills.Children.Clear();
            int ID = 0;
            Grid innerGrid;
            foreach (string skill in editedSkillset.SkillsList)
            {
                innerGrid = new Grid();
                innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                innerGrid.ColumnDefinitions.Add(new ColumnDefinition());
                innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                innerGrid.RowDefinitions.Add(new RowDefinition());
                TextBlock tblSkill = new TextBlock();
                tblSkill.Text = skill;
                Grid.SetRow(tblSkill, 0);
                Grid.SetColumn(tblSkill, 0);
                Button removeButton = new Button();
                removeButton.Name = "btnRemoveSkill" + ID;
                removeButton.DataContext = skill;
                removeButton.Content = new Image() { Source = removeButtonImage };
                removeButton.Width = 25;
                removeButton.Background = Brushes.Transparent;
                removeButton.BorderThickness = new Thickness(0);
                removeButton.Click += SkillRemoveButton_Click;
                removeButton.HorizontalAlignment = HorizontalAlignment.Right;
                Grid.SetRow(removeButton, 0);
                Grid.SetColumn(removeButton, 1);
                Button editButton = new Button();
                editButton.Name = "btnEditSkill" + ID;
                editButton.DataContext = skill;
                editButton.Content = new Image() { Source = editButtonImage };
                editButton.Width = 25;
                editButton.Background = Brushes.Transparent;
                editButton.BorderThickness = new Thickness(0);
                editButton.Click += SkillEditButton_Click;
                editButton.HorizontalAlignment = HorizontalAlignment.Right;
                Grid.SetRow(editButton, 0);
                Grid.SetColumn(editButton, 2);
                innerGrid.Children.Add(tblSkill);
                innerGrid.Children.Add(removeButton);
                innerGrid.Children.Add(editButton);
                stckpnlSkills.Children.Add(innerGrid);
                ID++;
            }
        }

        private void btnAddSkill_Click(object sender, RoutedEventArgs e)
        {
            SkillEditWindow skillEditWindow = new SkillEditWindow();
            skillEditWindow.ShowDialog();
            if (skillEditWindow.DialogResult == true)
            {
                editedSkillset.SkillsList.Add(skillEditWindow.editedSkill);
                GenerateSkillList();
            }
        }

        private void SkillEditButton_Click(object sender, RoutedEventArgs e)
        {
            SkillEditWindow skillEditWindow = new SkillEditWindow((sender as Button).DataContext as string);
            skillEditWindow.ShowDialog();
            if (skillEditWindow.DialogResult == true)
            {
                editedSkillset.SkillsList.Remove((sender as Button).DataContext as string);
                editedSkillset.SkillsList.Add(skillEditWindow.editedSkill);
                GenerateSkillList();
            }
        }

        private void SkillRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            editedSkillset.SkillsList.Remove((sender as Button).DataContext as string);
            GenerateSkillList();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; 
            this.Close();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            editedSkillset.MainSkill = txtSkillset.Text;
            this.DialogResult = true;
            this.Close();
        }
    }
}
