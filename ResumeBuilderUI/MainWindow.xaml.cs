using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.Json;
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

namespace ResumeBuilderUI
{
    public partial class MainWindow : Window
    {
        ApplicantProfile activeProfile;
        StackPanel? innerStack;
        StackPanel? skillsStack;
        StackPanel? experienceStack;
        StackPanel? contactsStack;
        StackPanel? affiliationsStack;

        public MainWindow()
        {
            InitializeComponent();
            ReadConfigFile();
            InitializeUIElements();
        }

        //Method to read config file
        private void ReadConfigFile()
        {
            activeProfile= new ApplicantProfile();
            if (File.Exists("Config.ini"))
            {
                try
                {
                    using(StreamReader configReader = new StreamReader("Config.ini"))
                    {
                        string line = configReader.ReadLine();
                        while (line!=null)
                        {
                            switch (line)
                            {
                                case "[ActiveUserProfile]":
                                    line= configReader.ReadLine();
                                    while (line != null) {
                                        string[] elementsOfLine = line.Split(" = ");
                                        if (elementsOfLine[1]!="" && File.Exists(System.IO.Path.Combine(@"Data\", elementsOfLine[1])))
                                        {
                                            using (StreamReader profileReader = new StreamReader(
                                                System.IO.Path.Combine(@"Data\", elementsOfLine[1])))
                                            {
                                                string profileJsonLine = profileReader.ReadToEnd();
                                                activeProfile = JsonSerializer.Deserialize<ApplicantProfile>(profileJsonLine);
                                            }
                                        }
                                        else
                                        {
                                            //MessageBox.Show("User profile not found. Create new one?");
                                        }
                                        line = configReader.ReadLine();
                                    }
                                    break;
                                default: break;
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                InitializeConfigFile();
            }
 
        }

        private void InitializeConfigFile()
        {
            using (StreamWriter configWriter = new StreamWriter("Config.ini", false))
            {
                configWriter.WriteLine("[ActiveUserProfile]");
                configWriter.WriteLine("ProfileFile = ");
            }

        }

        private void InitializeUIElements()
        {
            GenerateTextboxForName(activeProfile.Name + " " + activeProfile.Surname);
            GenerateCheckboxesForSkills(activeProfile.skillsList);
            GenerateCheckboxesForEmployment(activeProfile.employmentsList);
            GenerateCheckboxesForContacts(activeProfile.contactsList);
            GenerateCheckboxesForAffiliations(activeProfile.affiliationsList);
            GenerateTooltipsForSkillsCheckBoxes(activeProfile.employmentsList);
            Button btnGenerate = new Button { Content = "Generate Application" };
            btnGenerate.Click += GenerateResumeButton_Click;
            Label btnGenerateLable = new Label { Content = "", Target = btnGenerate };
            stckpnlMain.Children.Add(btnGenerateLable);
            stckpnlMain.Children.Add(btnGenerate);
        }

        private void GenerateTextboxForName(string name)
        {
            Label nameLabel = new Label { Content = "_Name" };
            TextBox nameTextbox = new TextBox { Name="txtName" };
            nameTextbox.Text = name;
            nameLabel.Target = nameTextbox;
            stckpnlMain.Children.Add(nameLabel);
            stckpnlMain.Children.Add(nameTextbox);
        }

        private void GenerateCheckboxesForSkills(Dictionary<string, string> skills)
        {
            Expander skillsExpander = new Expander();
            skillsExpander.Header = "Skills";
            List<string> skillCategories = skills.Values.ToList();
            skillCategories = skillCategories.Distinct().ToList();
            skillsStack = new StackPanel{Orientation = Orientation.Horizontal};
            int categoryCounter = 0;
            int skillCounter = 0;
            foreach (string skillCategory in skillCategories)
            {
                categoryCounter++;
                CheckBox categoryCheckbox = new CheckBox();
                categoryCheckbox.Name = "Checkbox" + categoryCounter.ToString();
                categoryCheckbox.Content = skillCategory.Substring(1);
                categoryCheckbox.Margin = new Thickness { Left = 10 , Top = 5};
                innerStack = new StackPanel{Orientation = Orientation.Vertical};
                innerStack.Children.Add(categoryCheckbox);
                foreach (var skill in skills)
                {
                    skillCounter++;
                    if (skill.Value.Equals(skillCategory))
                    {
                        CheckBox skillCheckbox = new CheckBox();
                        skillCheckbox.Name = "Checkbox"+categoryCounter.ToString()+"_"+skillCounter.ToString();
                        skillCheckbox.Content = skill.Key.Substring(1);
                        skillCheckbox.Margin = new Thickness {Left = 20 };
                        if (skill.Key.StartsWith('+'))
                        {
                            skillCheckbox.IsChecked = true;
                        }
                        innerStack.Children.Add(skillCheckbox);
                    }
                }
                skillsStack.Children.Add(innerStack);
                skillCounter = 0;
            }
            Button skillAutoAssignButton = new Button();
            skillAutoAssignButton.Name = "btnSkillAutoAssign";
            skillAutoAssignButton.Content = "Automatic Skill Assignment";
            skillAutoAssignButton.Margin = new Thickness(20);
            innerStack = new StackPanel { Orientation = Orientation.Vertical};
            innerStack.Children.Add(skillsStack);
            innerStack.Children.Add(skillAutoAssignButton);
            skillsExpander.Margin = new Thickness { Top = 10 };
            skillsExpander.IsExpanded = true;
            skillsExpander.Content = innerStack;
            stckpnlMain.Children.Add(skillsExpander);
        }

        private void GenerateTooltipsForSkillsCheckBoxes(List<Employment> employmentList)
        {
            int counterOfEmploymentsWIthTag;
            foreach(StackPanel panelWithCheckbox in skillsStack.Children)
            {
                foreach(CheckBox skillCheckbox in panelWithCheckbox.Children)
                {
                    skillCheckbox.Foreground = Brushes.Black;
                    counterOfEmploymentsWIthTag = 0;
                    skillCheckbox.ToolTip = "";
                    foreach(Employment employment in employmentList)
                    {
                        foreach((string, string) experience in employment.Experience)
                        {
                            if (skillCheckbox.Content.Equals(experience.Item1))
                            {
                                counterOfEmploymentsWIthTag++;
                                skillCheckbox.ToolTip+= employment.Employer+" "+employment.Title+" \n";
                            }
                        }
                    }
                    if (counterOfEmploymentsWIthTag== 0)
                    {
                        skillCheckbox.Foreground = Brushes.Red;
                    }
                    skillCheckbox.ToolTip += counterOfEmploymentsWIthTag +" Employments with this tag";
                }
            }
        }

        private List<string> ReadActiveSkillCheckBoxes()
        {
            List<string> relevantSkillsCheckboxes = new List<string>();
            foreach (StackPanel panelWithCheckbox in skillsStack.Children)
            {
                foreach (CheckBox skillCheckbox in panelWithCheckbox.Children)
                {
                    if (skillCheckbox.IsChecked == true && skillCheckbox.Content != null)
                    {
                        relevantSkillsCheckboxes.Add(skillCheckbox.Content.ToString());
                    }
                }
            }
            return relevantSkillsCheckboxes;
        }

        private void GenerateCheckboxesForEmployment(List<Employment> employmentList)
        {
            experienceStack = new StackPanel();
            Expander experienceExpander = new Expander();
            experienceExpander.Header = "Work Experience";
            employmentList = Employment.SortListOfEmployments(employmentList);
            int ID = 0;
            foreach (Employment employment in employmentList)
            {
                Grid innerGrid = new Grid {Margin= new Thickness {Top=5, Bottom=5 } };
                innerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width=GridLength.Auto});
                innerGrid.ColumnDefinitions.Add(new ColumnDefinition());
                innerGrid.RowDefinitions.Add(new RowDefinition());
                CheckBox employmentCheckbox = new CheckBox();
                employmentCheckbox.Name = "employmentCheckbox" + ID;
                employmentCheckbox.Content = employment.ToString("en_US");
                employmentCheckbox.Margin = new Thickness {Left = 10, Top = 5, Right = 10, Bottom= 5 };
                employmentCheckbox.IsChecked = true;
                employmentCheckbox.Click += UpdateSkillsTooltipsCheckbox_Click;
                Grid.SetRow(employmentCheckbox, 0);
                Grid.SetColumn(employmentCheckbox, 0);
                Button editEmploymentButton = new Button();
                editEmploymentButton.Name = "employmentEditButton" + ID;
                editEmploymentButton.Content = "Edit";
                editEmploymentButton.Click += EditEmploymentButton_Click;
                editEmploymentButton.DataContext = employment;
                editEmploymentButton.Width= 100;
                editEmploymentButton.HorizontalAlignment= HorizontalAlignment.Right;
                Grid.SetRow(editEmploymentButton, 0);
                Grid.SetColumn(editEmploymentButton, 1);
                innerGrid.Children.Add(employmentCheckbox);
                innerGrid.Children.Add(editEmploymentButton);
                experienceStack.Children.Add(innerGrid);
                ID++;
            }
            experienceExpander.Margin = new Thickness { Top = 10 };
            experienceExpander.IsExpanded= false;
            experienceExpander.Content= experienceStack;
            stckpnlMain.Children.Add(experienceExpander);
        }

        private List<Employment> ReadActiveEmploymentCheckBoxes()
        {
            List<Employment> employmentList = new List<Employment>();
            foreach (Grid gridWithEmployment in experienceStack.Children)
            {
                if ((gridWithEmployment.Children[0] as CheckBox).IsChecked == true) { 
                    employmentList.Add((gridWithEmployment.Children[1] as Button).DataContext as Employment);
                }
            }
            return employmentList;
        }

        private void GenerateCheckboxesForContacts (Dictionary<string, string> contactsList)
        {
            contactsStack = new StackPanel();
            Expander contactsExpander = new Expander();
            contactsExpander.Header= "Contacts";
            foreach (string contactOption in contactsList.Keys)
            {
                CheckBox contactCheckbox = new CheckBox();
                contactCheckbox.Name = "contactCheckbox" + contactOption;
                contactCheckbox.Content = contactOption +": "+  contactsList.GetValueOrDefault(contactOption);
                contactCheckbox.Margin = new Thickness {Left = 10, Top=5 };
                contactCheckbox.IsChecked = true;
                contactsStack.Children.Add(contactCheckbox);
            }
            contactsExpander.Margin = new Thickness {Top = 10 };
            contactsExpander.Content = contactsStack;
            contactsExpander.IsExpanded = false;
            stckpnlMain.Children.Add(contactsExpander);
        }

        private Dictionary<string, string> ReadActiveContactCheckBoxes()
        {
            Dictionary<string, string> activeContacts = new Dictionary<string, string>();
            foreach (CheckBox contactCheckbox in contactsStack.Children)
            {
                if (contactCheckbox.IsChecked== true)
                {
                    activeContacts.Add(contactCheckbox.Name.ToString().Substring(15), 
                        contactCheckbox.Content.ToString().Substring(contactCheckbox.Name.ToString().Length-13));
                }
            }
            return activeContacts;
        }

        private void GenerateCheckboxesForAffiliations(List<string> affiliationsList)
        {
            affiliationsStack = new StackPanel();
            Expander affiliationExpander = new Expander();
            affiliationExpander.Header = "Professional Affiliations";
            int ID = 0;
            foreach (string affiliation in affiliationsList)
            {
                CheckBox affiliationCheckbox = new CheckBox();
                affiliationCheckbox.Name = "affiliationCheckbox" + ID.ToString();
                affiliationCheckbox.Content = affiliation;
                affiliationCheckbox.Margin = new Thickness { Left = 10, Top = 5 };
                affiliationCheckbox.IsChecked = true;
                affiliationsStack.Children.Add(affiliationCheckbox);
                ID++;
            }
            affiliationExpander.Margin = new Thickness { Top = 10 };
            affiliationExpander.Content = affiliationsStack;
            affiliationExpander.IsExpanded = false;
            stckpnlMain.Children.Add(affiliationExpander);
        }

        private List<string> ReadActiveAffiliationsCheckBoxes()
        {
            List<string> affiliationsList = new List<string>();
            foreach (CheckBox contactCheckbox in affiliationsStack.Children)
            {
                if (contactCheckbox.IsChecked == true)
                {
                    affiliationsList.Add(contactCheckbox.Content.ToString());
                }
            }
            return affiliationsList;
        }

        private void EditEmploymentButton_Click(object sender, RoutedEventArgs e)
        {
            EmploymentEditWindow employmentEditWindow = new EmploymentEditWindow((sender as Button).DataContext as Employment);
            employmentEditWindow.ShowDialog();
            if (employmentEditWindow.DialogResult == true)
            {

            }
        }

        private void UpdateSkillsTooltipsCheckbox_Click(object sender, RoutedEventArgs e)
        {
            GenerateTooltipsForSkillsCheckBoxes(ReadActiveEmploymentCheckBoxes());
        }

        private void GenerateResumeButton_Click(object sender, RoutedEventArgs e)
        {
            ResumeBuilder builder = new ResumeBuilder("D:\\Programming\\VisualStudioProjects\\ResumeBuilderUI\\ResumeBuilderUI\\bin\\Debug\\net6.0-windows\\resources\\ResumeResourceEng.txt");
            //builder.Name = txtName.Text;
            builder.Title = txtTitle.Text;
            builder.ContactsList = ReadActiveContactCheckBoxes();
            builder.RelevantSkills = ReadActiveSkillCheckBoxes();
            builder.EmploymentsList= ReadActiveEmploymentCheckBoxes();
            builder.AffiliationsList = ReadActiveAffiliationsCheckBoxes();
            builder.BuildResume("CV "+ builder.Name+" - "+ builder.Title+".pdf");

            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, builder);
            }
        }

        private void ChangeLanguageMenuButton_Click(object sender, RoutedEventArgs e)
        {
            menuEnglish.IsChecked = sender.Equals(menuEnglish);
            menuRussian.IsChecked = sender.Equals(menuRussian);
        }
    }
}
