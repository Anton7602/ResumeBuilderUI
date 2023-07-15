using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
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
        private ApplicantProfile activeProfile;
        private TextBlock nameTextBlock;
        private TextBox titleTextBox;
        private WrapPanel skillWrapPanel;
        private StackPanel experienceStack;
        private StackPanel contactsStack;
        private StackPanel affiliationsStack;
        private BitmapImage addButtonImage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\resources\addButton.png"));
        private BitmapImage editButtonImage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\resources\editButton.png"));
        private BitmapImage removeButtonImage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\resources\removeButton.png"));

        public MainWindow()
        {
            InitializeComponent();
            ReadConfigFile();
            InitializeUIElements();
        }

        //Method to read config file
        private void ReadConfigFile()
        {
            //activeProfile= new ApplicantProfile();
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
                                        if (elementsOfLine[1]!="null" && File.Exists(@"profiles\"+ elementsOfLine[1]+ ".cvp"))
                                        {
                                            using (StreamReader profileReader = new StreamReader(@"profiles\"+ elementsOfLine[1]+ ".cvp"))
                                            {
                                                string profileJsonLine = profileReader.ReadToEnd();
                                                activeProfile = JsonSerializer.Deserialize<ApplicantProfile>(profileJsonLine);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Default user profile not found. Select or create new?");
                                            activeProfile = new ApplicantProfile();
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
                if (activeProfile != null)
                {
                    configWriter.WriteLine("ProfileFile = " + activeProfile.Name + activeProfile.ID);
                }
                else
                { configWriter.WriteLine("ProfileFile = null"); }
            }

        }

        private void InitializeUIElements()
        {
            stckpnlMain.Children.Clear();
            GenerateTextboxForName(activeProfile.Name);
            if (activeProfile.TitlesList.Count > 0) {
                GenerateTextboxForTitle(activeProfile.TitlesList.Last());
            }
            else { GenerateTextboxForTitle(""); }
            GenerateCheckboxesForSkills(activeProfile.skillsList);
            GenerateCheckboxesForEmployment(activeProfile.employmentsList);
            GenerateCheckboxesForContacts(activeProfile.contactsList);
            GenerateCheckboxesForAffiliations(activeProfile.affiliationsList);
            GenerateTooltipsForSkillsCheckBoxes(activeProfile.employmentsList);
            GenerateApplicationBuilderButton();
        }

        private void GenerateTextboxForName(string name)
        {
            Label nameLabel = new Label { Content = "Name:" };
            nameTextBlock = new TextBlock { Name="txtName" };
            nameTextBlock.Text = name;
            nameLabel.Target = nameTextBlock;
            stckpnlMain.Children.Add(nameLabel);
            stckpnlMain.Children.Add(nameTextBlock);
        }

        private void GenerateTextboxForTitle(string title)
        {
            Label titleLabel = new Label { Content = "Title:" };
            titleTextBox = new TextBox { Name = "txtTitle" };
            titleTextBox.Text = title;
            titleLabel.Target = titleTextBox;
            stckpnlMain.Children.Add(titleLabel);
            stckpnlMain.Children.Add(titleTextBox);
        }

        private void GenerateCheckboxesForSkills(Dictionary<string, string> skills)
        {
            Expander skillsExpander = new Expander();
            Grid headerGrid = GenerateExpanderHeaderWithAddButton("Skills:", "skills");
            skillsExpander.Header = headerGrid;
            (headerGrid.Children[1] as Button).Click += AddNewSkillButton_Click;
            StackPanel innerStack = new StackPanel();
            List<string> skillCategories = skills.Values.ToList();
            skillCategories = skillCategories.Distinct().ToList();
            skillWrapPanel = new WrapPanel();
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
                skillWrapPanel.Children.Add(innerStack);
                skillCounter = 0;
            }
            Button skillAutoAssignButton = new Button();
            skillAutoAssignButton.Name = "btnSkillAutoAssign";
            skillAutoAssignButton.Content = "Automatic Skill Assignment";
            skillAutoAssignButton.Margin = new Thickness(20);
            innerStack = new StackPanel { Orientation = Orientation.Vertical};
            innerStack.Children.Add(skillWrapPanel);
            innerStack.Children.Add(skillAutoAssignButton);
            skillsExpander.Margin = new Thickness { Top = 10 };
            skillsExpander.IsExpanded = activeProfile.expanderStates[0];
            skillsExpander.Content = innerStack;
            stckpnlMain.Children.Add(skillsExpander);
        }

        private void GenerateTooltipsForSkillsCheckBoxes(List<Employment> employmentList)
        {
            int counterOfEmploymentsWIthTag;
            foreach(StackPanel panelWithCheckbox in skillWrapPanel.Children)
            {
                foreach(CheckBox skillCheckbox in panelWithCheckbox.Children)
                {
                    skillCheckbox.Foreground = Brushes.Black;
                    counterOfEmploymentsWIthTag = 0;
                    skillCheckbox.ToolTip = "";
                    foreach(Employment employment in employmentList)
                    {
                        foreach(Experience experience in employment.ExperiencesList)
                        {
                            if (skillCheckbox.Content.Equals(experience.Tag))
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
            foreach (StackPanel panelWithCheckbox in skillWrapPanel.Children)
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
            //Making sure stackPanel with experience is empty
            experienceStack = new StackPanel();
            //Setting up Expander element
            Expander experienceExpander = new Expander();
            //Setting up Expander header with "Add new employment" button
            Grid headerGrid = GenerateExpanderHeaderWithAddButton("Work Experience:", "experience");
            experienceExpander.Header = headerGrid;
            (headerGrid.Children[1] as Button).Click += AddNewEmploymentButton_Click;
            //Setting up Expander parameters
            experienceExpander.Margin = new Thickness { Top = 10 };
            experienceExpander.IsExpanded = activeProfile.expanderStates[1];
            experienceExpander.Content = experienceStack;
            //Case if Applicant Profile has Employments.
            if (employmentList.Count > 0)
            {
                employmentList = Employment.SortListOfEmployments(employmentList);
                int ID = 0;
                //Setting up each employment in applicant profile with edit and delete button
                foreach (Employment employment in employmentList)
                {
                    //Setting up grid parameters to hold employment info, remove and edit buttons
                    Grid innerGrid = GenerateItemListWithEditAndRemoveButtons(employment.ToString("en_US"), "Employment" + ID);
                    (innerGrid.Children[0] as CheckBox).Click += UpdateSkillsTooltipsCheckbox_Click;
                    (innerGrid.Children[1] as Button).Click += EditEmploymentButton_Click;
                    (innerGrid.Children[1] as Button).DataContext = employment;
                    (innerGrid.Children[2] as Button).Click += RemoveEmploymentButton_Click;
                    (innerGrid.Children[2] as Button).DataContext = employment;
                    experienceStack.Children.Add(innerGrid);
                    ID++;
                }
            }
            else
            //Case employment list of applicant profile is empty
            {
                experienceStack.Children.Add(GenerateEmptyListMessage("employment"));
            }
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
            Grid headerGrid = GenerateExpanderHeaderWithAddButton("Contacts:", "contacts");
            contactsExpander.Header = headerGrid;
            (headerGrid.Children[1] as Button).Click += AddNewContactButton_Click;
            contactsExpander.IsExpanded = activeProfile.expanderStates[2];
            contactsExpander.Content = contactsStack;
            if (activeProfile.contactsList.Count > 0)
            {
                foreach (string contactOption in contactsList.Keys)
                {
                    Grid innerGrid = GenerateItemListWithEditAndRemoveButtons(
                        contactOption + ": " + contactsList.GetValueOrDefault(contactOption), "contact");
                    (innerGrid.Children[1] as Button).Click += EditContactButton_Click;
                    (innerGrid.Children[1] as Button).DataContext = contactOption;
                    (innerGrid.Children[2] as Button).Click += RemoveContactButton_Click;
                    (innerGrid.Children[2] as Button).DataContext = contactOption;
                    contactsStack.Children.Add(innerGrid);
                }
                contactsExpander.Margin = new Thickness { Top = 10 };
                contactsExpander.Content = contactsStack;
            }
            else
            {
                contactsStack.Children.Add(GenerateEmptyListMessage("contact"));
            }
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

        private void GenerateCheckboxesForAffiliations(List<ProffessionalAffiliation> affiliationsList)
        {
            affiliationsStack = new StackPanel();
            Expander affiliationExpander = new Expander();
            Grid headerGrid = GenerateExpanderHeaderWithAddButton("Professional Affiliations:", "affiliations");
            affiliationExpander.Header = headerGrid;
            affiliationExpander.IsExpanded = activeProfile.expanderStates[3];
            (headerGrid.Children[1] as Button).Click += AddNewAffiliationButton_Click;
            affiliationExpander.Margin = new Thickness { Top = 10 };
            affiliationExpander.Content = affiliationsStack;
            stckpnlMain.Children.Add(affiliationExpander);
            if (activeProfile.affiliationsList.Count > 0)
            {
                activeProfile.affiliationsList = ProffessionalAffiliation.SortListOfAffiliations(activeProfile.affiliationsList);
                int ID = 0;
                foreach (ProffessionalAffiliation affiliation in affiliationsList)
                {
                    Grid innerGrid = GenerateItemListWithEditAndRemoveButtons(affiliation.ToString(), "Affiliation" + ID);
                    (innerGrid.Children[0] as CheckBox).Click += UpdateSkillsTooltipsCheckbox_Click;
                    (innerGrid.Children[1] as Button).Click += EditAffiliationButton_Click;
                    (innerGrid.Children[1] as Button).DataContext = affiliation;
                    (innerGrid.Children[2] as Button).Click += RemoveAffiliationButton_Click;
                    (innerGrid.Children[2] as Button).DataContext = affiliation;
                    affiliationsStack.Children.Add(innerGrid);
                    ID++;
                }
            }
            else
            {
                affiliationsStack.Children.Add(GenerateEmptyListMessage("affiliation"));
            }
        }

        private List<ProffessionalAffiliation> ReadActiveAffiliationsCheckBoxes()
        {
            List<ProffessionalAffiliation> affiliationsList = new List<ProffessionalAffiliation>();
            foreach (CheckBox contactCheckbox in affiliationsStack.Children)
            {
                if (contactCheckbox.IsChecked == true)
                {
                    affiliationsList.Add(ProffessionalAffiliation.Parse(contactCheckbox.Content.ToString()));
                }
            }
            return affiliationsList;
        }

        private Grid GenerateExpanderHeaderWithAddButton(string headerText, string headerName)
        {
            Grid headerGrid= new Grid();
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            headerGrid.RowDefinitions.Add(new RowDefinition());
            TextBlock headerTitle = new TextBlock { Text = headerText, Name = headerName };
            Button headerButton = GenerateImageButtonParameters("btnAdd"+headerName, addButtonImage);
            headerButton.Width = 20;
            Grid.SetRow(headerTitle, 0);
            Grid.SetColumn(headerTitle, 0);
            Grid.SetRow(headerButton, 0);
            Grid.SetColumn(headerButton, 1);
            headerGrid.Children.Add(headerTitle);
            headerGrid.Children.Add(headerButton);
            return headerGrid;
        }

        private void GenerateApplicationBuilderButton()
        {
            Button btnGenerate = new Button { Content = "Generate Application" };
            btnGenerate.Click += GenerateResumeButton_Click;
            Label btnGenerateLable = new Label { Content = "", Target = btnGenerate };
            stckpnlMain.Children.Add(btnGenerateLable);
            stckpnlMain.Children.Add(btnGenerate);
        }

        private Grid GenerateItemListWithEditAndRemoveButtons(string itemListContent, string itemName)
        {
            //Setting up grid
            Grid innerGrid = new Grid { Margin = new Thickness { Top = 5, Bottom = 5 } };
            innerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            innerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            innerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            innerGrid.RowDefinitions.Add(new RowDefinition());
            //Setting up Checkbox parameters
            CheckBox employmentCheckbox = new CheckBox();
            employmentCheckbox.Name = itemName;
            employmentCheckbox.Content = itemListContent;
            employmentCheckbox.Margin = new Thickness { Left = 10, Top = 5, Right = 10, Bottom = 5 };
            employmentCheckbox.IsChecked = true;
            Grid.SetRow(employmentCheckbox, 0);
            Grid.SetColumn(employmentCheckbox, 0);
            //Setting up Edit button
            Button editEmploymentButton = GenerateImageButtonParameters("editButton" +itemName, editButtonImage);
            Grid.SetRow(editEmploymentButton, 0);
            Grid.SetColumn(editEmploymentButton, 2);
            //Setting up Remove button
            Button removeEmploymentButton = GenerateImageButtonParameters("removeButton" + itemName, removeButtonImage);
            Grid.SetRow(removeEmploymentButton, 0);
            Grid.SetColumn(removeEmploymentButton, 1);
            innerGrid.Children.Add(employmentCheckbox);
            innerGrid.Children.Add(editEmploymentButton);
            innerGrid.Children.Add(removeEmploymentButton);
            return innerGrid;
        }

        private Button GenerateImageButtonParameters(string buttonName, BitmapImage buttonImage)
        {
            Button generatedButton = new Button
            {
            Name = buttonName,
            Content = new Image() { Source = buttonImage },
            Width = 25,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            HorizontalAlignment = HorizontalAlignment.Right
        };
            return generatedButton;
        }

        private TextBlock GenerateEmptyListMessage(string listName)
        {
            TextBlock messageTxtBlock = new TextBlock();
            messageTxtBlock.Text = "No " +listName + "'s in current profile. Click + button to add new";
            messageTxtBlock.Margin = new Thickness(10);
            return messageTxtBlock;
        }

        private void SaveExpanderStatesToProfile()
        {
            activeProfile.expanderStates.Clear();
            foreach(var element in stckpnlMain.Children)
            {
                if (element is Expander)
                {
                    activeProfile.expanderStates.Add((element as Expander).IsExpanded);
                }
            }
        }

        private void AddNewSkillButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RemoveEmploymentButton_Click(object sender, RoutedEventArgs e)
        {
            List<Employment> updatedEmploymentList = new List<Employment>();
            foreach (Employment employment in activeProfile.employmentsList)
            {
                if (!employment.Equals((sender as Button).DataContext as Employment))
                {
                    updatedEmploymentList.Add(employment);
                }
            }
            activeProfile.employmentsList= updatedEmploymentList;
            SaveExpanderStatesToProfile();
            InitializeUIElements();
        }
        private void EditEmploymentButton_Click(object sender, RoutedEventArgs e)
        {
            EmploymentEditWindow employmentEditWindow = new EmploymentEditWindow((sender as Button).DataContext as Employment);
            employmentEditWindow.ShowDialog();
            if (employmentEditWindow.DialogResult == true)
            {
                activeProfile.employmentsList.Remove((sender as Button).DataContext as Employment);
                activeProfile.employmentsList.Add(employmentEditWindow.editedEmployment.Clone());
                SaveExpanderStatesToProfile();
                InitializeUIElements();
            }
        }
        private void AddNewEmploymentButton_Click(object sender, RoutedEventArgs e)
        {
            EmploymentEditWindow employmentEditWindow = new EmploymentEditWindow();
            employmentEditWindow.ShowDialog();
            if (employmentEditWindow.DialogResult == true)
            {
                activeProfile.employmentsList.Add(employmentEditWindow.editedEmployment);
                SaveExpanderStatesToProfile();
                InitializeUIElements();
            }
        }

        private void AddNewContactButton_Click(object sender, RoutedEventArgs e)
        {
            ContactEditWindow contactEditWindow = new ContactEditWindow();
            contactEditWindow.ShowDialog();
            if (contactEditWindow.DialogResult == true)
            {
                activeProfile.contactsList.Add(contactEditWindow.editedContact.Item1, contactEditWindow.editedContact.Item2);
                SaveExpanderStatesToProfile();
                InitializeUIElements();
            }
        }

        private void RemoveContactButton_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> updatedContactList = new Dictionary<string, string>();  
            foreach(var contact in activeProfile.contactsList)
            {
                if(!contact.Key.Equals((sender as Button).DataContext.ToString()))
                {
                    updatedContactList.Add(contact.Key, contact.Value);
                }
            }
            activeProfile.contactsList = updatedContactList;
            SaveExpanderStatesToProfile();  
            InitializeUIElements();
        }

        private void EditContactButton_Click(object sender, RoutedEventArgs e)
        {
            string editedContactKey = (sender as Button).DataContext as string;
            string editedContactValue = activeProfile.contactsList.GetValueOrDefault(editedContactKey);
            ContactEditWindow contactEditWindow = new ContactEditWindow((editedContactKey, editedContactValue));
            contactEditWindow.ShowDialog();
            if (contactEditWindow.DialogResult == true)
            {
                activeProfile.contactsList.Remove(editedContactKey);
                activeProfile.contactsList.Add(contactEditWindow.editedContact.Item1, contactEditWindow.editedContact.Item2);
                SaveExpanderStatesToProfile();
                InitializeUIElements();
            }
        }

        private void AddNewAffiliationButton_Click(object sender, RoutedEventArgs e)
        {
            AffiliationEditWindow affiliationEditWindow = new AffiliationEditWindow();
            affiliationEditWindow.ShowDialog();
            if (affiliationEditWindow.DialogResult == true)
            {
                activeProfile.affiliationsList.Add(affiliationEditWindow.editedAffiliation);
                SaveExpanderStatesToProfile();
                InitializeUIElements();
            }
        }
        private void RemoveAffiliationButton_Click(object sender, RoutedEventArgs e)
        {
            List<ProffessionalAffiliation> updatedAffiliationsList = new List<ProffessionalAffiliation>();
            foreach(ProffessionalAffiliation affiliation in activeProfile.affiliationsList)
            {
                if (!affiliation.Equals((sender as Button).DataContext as ProffessionalAffiliation))
                {
                    updatedAffiliationsList.Add(affiliation);
                }
            }
            activeProfile.affiliationsList = updatedAffiliationsList;
            SaveExpanderStatesToProfile();
            InitializeUIElements();
        }

        private void EditAffiliationButton_Click(object sender, RoutedEventArgs e)
        {
            AffiliationEditWindow affiliationEditWindow = new AffiliationEditWindow((sender as Button).DataContext as ProffessionalAffiliation);
            affiliationEditWindow.ShowDialog();
            if (affiliationEditWindow.DialogResult == true)
            {
                activeProfile.affiliationsList.Remove((sender as Button).DataContext as ProffessionalAffiliation);
                activeProfile.affiliationsList.Add(affiliationEditWindow.editedAffiliation);
                SaveExpanderStatesToProfile();
                InitializeUIElements();
            }
        }

        private void UpdateSkillsTooltipsCheckbox_Click(object sender, RoutedEventArgs e)
        {
            GenerateTooltipsForSkillsCheckBoxes(ReadActiveEmploymentCheckBoxes());
        }

        private void GenerateResumeButton_Click(object sender, RoutedEventArgs e)
        {
            ResumeBuilder builder = new ResumeBuilder("D:\\Programming\\VisualStudioProjects\\ResumeBuilderUI\\ResumeBuilderUI\\bin\\Debug\\net6.0-windows\\resources\\ResumeResourceEng.txt");
            builder.Name = activeProfile.Name;
            builder.Title = titleTextBox.Text;
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

        private void SaveProfileChangesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!activeProfile.TitlesList.Contains(titleTextBox.Text))
            {
                activeProfile.TitlesList.Add(titleTextBox.Text);
            }
            SaveExpanderStatesToProfile();
            using (StreamWriter profileWriter = new StreamWriter(@"profiles\" + activeProfile.Name + activeProfile.ID + ".cvp", false))
            {
                profileWriter.WriteLine(JsonSerializer.Serialize(activeProfile));
            }
            InitializeConfigFile();
        }
    }
}
