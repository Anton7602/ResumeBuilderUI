using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ResumeBuilderUI
{
    public partial class EmploymentEditWindow : Window
    {
        public Employment editedEmployment;
        private BitmapImage addButtonImage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\resources\addButton.png"));
        private BitmapImage editButtonImage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\resources\editButton.png"));
        private BitmapImage removeButtonImage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\resources\removeButton.png"));

        public EmploymentEditWindow()
        {
            InitializeComponent();
            btnNewExperience.Content = new Image() { Source = addButtonImage };
            editedEmployment= new Employment();
        }

        public EmploymentEditWindow(Employment employment)
        {
            InitializeComponent();
            editedEmployment= new Employment(employment);
            editedEmployment.ExperiencesList = Experience.Sort(editedEmployment.ExperiencesList);
            btnNewExperience.Content = new Image() { Source = addButtonImage };
            FillRecievedEmploymentsDataInFields();
        }

        private void FillRecievedEmploymentsDataInFields()
        {
            txtEmployerEdit.Text = editedEmployment.Employer;
            txtTitleEdit.Text = editedEmployment.Title;
            dateFrom.SelectedDate = editedEmployment.StartDate;
            dateTo.SelectedDate = editedEmployment.EndDate;
            GenerateExperienceView();
        }
        
        private void GenerateExperienceView()
        {
            stckpnlExperience.Children.Clear();
            int ID = 0;
            StackPanel innerStack;
            Grid innerGrid;
            foreach (Experience experience in editedEmployment.ExperiencesList)
            {
                innerStack = new StackPanel();
                innerGrid = new Grid();
                innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width=GridLength.Auto});
                innerGrid.ColumnDefinitions.Add(new ColumnDefinition());
                innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width=GridLength.Auto});
                innerGrid.RowDefinitions.Add(new RowDefinition());
                TextBlock tblExperienceTag = new TextBlock();
                tblExperienceTag.Text = experience.Tag;
                Grid.SetRow(tblExperienceTag, 0);
                Grid.SetColumn(tblExperienceTag, 0);
                Button removeButton = new Button();
                removeButton.Name = "btnRemoveExperience" + ID;
                removeButton.DataContext = experience;
                removeButton.Content = new Image() { Source = removeButtonImage };
                removeButton.Width= 25;
                removeButton.Background = Brushes.Transparent;
                removeButton.BorderThickness = new Thickness(0);
                removeButton.Click += ExperienceRemoveButton_Click;
                removeButton.HorizontalAlignment= HorizontalAlignment.Right;
                Grid.SetRow(removeButton, 0);
                Grid.SetColumn(removeButton, 1);
                Button editButton = new Button();
                editButton.Name = "btnEditExperience" + ID;
                editButton.DataContext = experience;
                editButton.Content = new Image() { Source = editButtonImage };
                editButton.Width = 25;
                editButton.Background = Brushes.Transparent;
                editButton.BorderThickness = new Thickness(0);
                editButton.Click += ExperienceEditButton_Click;
                editButton.HorizontalAlignment = HorizontalAlignment.Right;
                Grid.SetRow(editButton, 0);
                Grid.SetColumn(editButton, 2);
                TextBox experienceText = new TextBox();
                experienceText.Name = "txtExperience" + ID;
                experienceText.Text = experience.Description;
                experienceText.Margin = new Thickness(5);
                innerGrid.Children.Add(tblExperienceTag);
                innerGrid.Children.Add(removeButton);
                innerGrid.Children.Add(editButton);
                innerStack.Children.Add(innerGrid);
                innerStack.Children.Add(experienceText);
                stckpnlExperience.Children.Add(innerStack);
                ID++;
            }
        }

        private void SaveExperienceTextboxes()
        {
            foreach(StackPanel experienceView in stckpnlExperience.Children)
            {
                foreach(Experience experience in editedEmployment.ExperiencesList)
                {
                    if (experience.Equals(((experienceView.Children[0] as Grid).Children[1] as Button).DataContext as Experience))
                    {
                        experience.Description = (experienceView.Children[1] as TextBox).Text;
                        ((experienceView.Children[0] as Grid).Children[1] as Button).DataContext = experience;
                        ((experienceView.Children[0] as Grid).Children[2] as Button).DataContext = experience;
                    }
                }
            }
        }

        private void btnNewExperience_Click(object sender, RoutedEventArgs e)
        {
            ExperienceTagEditWindow experienceTagEditWindow = new ExperienceTagEditWindow();
            experienceTagEditWindow.ShowDialog();
            if (experienceTagEditWindow.DialogResult == true)
            {
                SaveExperienceTextboxes();
                editedEmployment.ExperiencesList.Add(new Experience(experienceTagEditWindow.editedExperienceTag, " "));
                GenerateExperienceView();
            }
        }
        private void ExperienceEditButton_Click(object sender, RoutedEventArgs e)
        {
            ExperienceTagEditWindow experienceTagEditWindow = new ExperienceTagEditWindow((sender as Button).DataContext as Experience);
            experienceTagEditWindow.ShowDialog();
            if (experienceTagEditWindow.DialogResult == true)
            {
                foreach(Experience experience in editedEmployment.ExperiencesList)
                {
                    if (experience.Equals((sender as Button).DataContext as Experience))
                    {
                        SaveExperienceTextboxes();
                        experience.Tag = experienceTagEditWindow.editedExperienceTag;
                        GenerateExperienceView();
                        break;
                    }
                }
            }
        }

        private void ExperienceRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveExperienceTextboxes();
            editedEmployment.ExperiencesList.Remove((sender as Button).DataContext as Experience);
            GenerateExperienceView();
        }


        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        { 
            editedEmployment.Employer=txtEmployerEdit.Text;
            editedEmployment.Title= txtTitleEdit.Text;
            editedEmployment.StartDate=(DateTime)dateFrom.SelectedDate;
            editedEmployment.EndDate=(DateTime)dateTo.SelectedDate;
            SaveExperienceTextboxes();
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
