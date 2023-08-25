using ResumeBuilderUI.Models;
using ResumeBuilderUI.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace ResumeBuilderUI.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private int LastSelectedNavBarIndex = 0;
        private ApplicantProfile _activeProfile = App.ActiveProfile;
        public ApplicantProfile ActiveProfile
        {
            get { return _activeProfile; }
            set
            {
                _activeProfile = value;
                OnPropertyChanged(nameof(ActiveProfile));
            }
        }
        private bool _isSettingsVisible = false;
        public bool IsSettingsVisible
        {
            get { return _isSettingsVisible; }
            set 
            { 
                if(value)
                {
                    IsUserVisible = false;
                    LastSelectedNavBarIndex = NavBarSelectionIndex;
                    NavBarSelectionIndex = -1;
                }
                if(!value && NavBarSelectionIndex==-1)
                {
                    NavBarSelectionIndex = LastSelectedNavBarIndex;
                }
                _isSettingsVisible = value;
                OnPropertyChanged("IsSettingsVisible");
            }
        }
        private bool _isUserVisible = false;
        public bool IsUserVisible
        {
            get { return _isUserVisible; }
            set
            {
                if (value)
                {
                    IsSettingsVisible = false;
                    LastSelectedNavBarIndex = NavBarSelectionIndex;
                    NavBarSelectionIndex = -1;
                }
                if (!value && NavBarSelectionIndex == -1)
                {
                    NavBarSelectionIndex = LastSelectedNavBarIndex;
                }
                _isUserVisible = value;
                OnPropertyChanged("IsUserVisible");
            }
        }

        private int _navBarSelectionIndex = 0;
        public int NavBarSelectionIndex
        {
            get { return _navBarSelectionIndex; }
            set
            {
                _navBarSelectionIndex= value;
                if (value>=0)
                {
                    IsSettingsVisible = false;
                    IsUserVisible = false;
                }
                OnPropertyChanged("NavBarSelectionIndex");
            }
        }

        public RelayCommand<object> BuildCVCommand { get; private set; }
        public RelayCommand<object> CloseAppCommand { get; private set; }

        public MainWindowViewModel()
        {
            BuildCVCommand = new RelayCommand<object>(BuildCV);
            CloseAppCommand = new RelayCommand<object>(CloseApp);
        }

        private void CloseApp(object commandParameter)
        {
            using (StreamWriter profileWriter = new StreamWriter(@"profiles\" + App.ActiveProfile.Name + App.ActiveProfile.ID + ".cvp", false))
            {
                profileWriter.WriteLine(JsonSerializer.Serialize(App.ActiveProfile));
            }
            ResumeBuilderUI.Properties.Settings.Default.DefaultProfile = (App.ActiveProfile.Name + App.ActiveProfile.ID + ".cvp");
            ResumeBuilderUI.Properties.Settings.Default.Save();
            (commandParameter as Window).Close();
        }

        private void BuildCV(object commandParameter)
        {
            ActiveProfile= App.ActiveProfile;
            ResumeBuilder CVbuilder = new ResumeBuilder(ActiveProfile.Name, ActiveProfile.TitlesList.First(), ActiveProfile.Summary,
                GetSelectedLanguagesList(), GetSelectedAffiliationsList(), GetSelectedSkillsList(), GetSelectedSkillsetsList(), 
                GetSelectedEmploymentsList(), GetSelectedEducationsList(), GetSelectedContactList());
            CVbuilder.BuildResume("CV " + CVbuilder.Name + " - " + CVbuilder.Title + ".pdf");
        }

        private List<Skillset> GetSelectedSkillsetsList()
        {
            List<Skillset> activeSkillsetsList = new List<Skillset>();
            foreach(Skillset skillset in ActiveProfile.SkillsetsList)
            {
                if (skillset.IsSelected)
                {
                    activeSkillsetsList.Add(skillset);
                }
            }
            return activeSkillsetsList;
        }

        private List<Education> GetSelectedEducationsList()
        {
            List<Education> activeEducationList = new List<Education>();
            foreach(Education education in ActiveProfile.EducationsList)
            {
                if(education.IsSelected)
                {
                    activeEducationList.Add(education);
                }
            }
            return activeEducationList;
        }

        private List<Contact> GetSelectedContactList()
        {
            List<Contact> activeContactList = new List<Contact>(); 
            foreach(Contact contact in ActiveProfile.ContactsList)
            {
                if(contact.IsSelected)
                {
                    activeContactList.Add(contact);
                }
            }
            return activeContactList;
        }

        private List<Employment> GetSelectedEmploymentsList()
        {
            List<Employment> activeEmploymentsList = new List<Employment>();
            foreach(Employment employment in ActiveProfile.EmploymentsList)
            {
                if(employment.IsSelected)
                {
                    activeEmploymentsList.Add(employment);
                }
            }
            return activeEmploymentsList;
        }

        private List<Skill> GetSelectedSkillsList()
        {
            List<Skill> activeSkillsList = new List<Skill>();
            foreach(Skillset skillset in ActiveProfile.SkillsetsList)
            {
                if(skillset.IsSelected)
                {
                    activeSkillsList.Add(new Skill(skillset.MainSkill));
                }
                foreach(Skill skill in skillset.SkillsList)
                {
                    if(skill.IsSelected)
                    {
                        activeSkillsList.Add(skill);
                    }
                }
            }
            return activeSkillsList;
        }

        private List<ProffessionalAffiliation> GetSelectedAffiliationsList()
        {
            List<ProffessionalAffiliation> activeAffiliationsList = new List<ProffessionalAffiliation>();
            foreach(ProffessionalAffiliation affiliation in ActiveProfile.AffiliationsList)
            {
                if(affiliation.IsSelected)
                {
                    activeAffiliationsList.Add(affiliation);
                }
            }
            return activeAffiliationsList;
        }

        private List<Language> GetSelectedLanguagesList()
        {
            List<Language> activeLanguageslist = new List<Language>();
            foreach(Language language in ActiveProfile.LanguagesList)
            {
                if(language.IsSelected)
                {
                    activeLanguageslist.Add(language);
                }
            }
            return activeLanguageslist;
        }
    }
}
