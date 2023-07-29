using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ResumeBuilderUI.Models
{
    [Serializable]
    public class ApplicantProfile
    {
        public long ID { get; set; }
        public string? Name { get; set; }
        public List<string> TitlesList { get; set; }
        public ObservableCollection<Employment> EmploymentsList { get; set; }
        public ObservableCollection<Skillset> SkillsetsList { get; set; }
        public ObservableCollection<Education> EducationsList { get; set; }
        public ObservableCollection<ProffessionalAffiliation> AffiliationsList { get; set; }
        public ObservableCollection<Language> LanguagesList { get; set; }
        public ObservableCollection<Contact> ContactsList { get; set; }

        public ApplicantProfile()
        {
            Name = "Placeholder";
            InitializeDefaultParameters();
            TitlesList.Add("Cool Fella");
            Skillset tempSet = new Skillset("CAD");
            tempSet.SkillsList.Add(new Skill("SolidWorks"));
            tempSet.SkillsList.Add(new Skill("AutoCAD"));
            tempSet.SkillsList.Add(new Skill("Siemens NX"));
            tempSet.SkillsList.Add(new Skill("Kompas-3D"));
            tempSet.SkillsList.Add(new Skill("Creo Parametrics"));
            tempSet.SkillsList.Add(new Skill("Pro Engineer"));
            tempSet.SkillsList.Add(new Skill("Autodesk Inventor"));
            tempSet.SkillsList.Add(new Skill("Catia"));
            SkillsetsList.Add(tempSet);
            tempSet = new Skillset("Programming");
            tempSet.SkillsList.Add(new Skill("Visual Studio"));
            tempSet.SkillsList.Add(new Skill("Android Studio"));
            SkillsetsList.Add(tempSet);
            tempSet = new Skillset("CAD");
            tempSet.SkillsList.Add(new Skill("SolidWorks"));
            tempSet.SkillsList.Add(new Skill("AutoCAD"));
            tempSet.SkillsList.Add(new Skill("Siemens NX"));
            tempSet.SkillsList.Add(new Skill("Kompas-3D"));
            tempSet.SkillsList.Add(new Skill("Creo Parametrics"));
            tempSet.SkillsList.Add(new Skill("Pro Engineer"));
            tempSet.SkillsList.Add(new Skill("Autodesk Inventor"));
            tempSet.SkillsList.Add(new Skill("Catia"));
            SkillsetsList.Add(tempSet);
            tempSet = new Skillset("Programming");
            tempSet.SkillsList.Add(new Skill("Visual Studio"));
            tempSet.SkillsList.Add(new Skill("Android Studio"));
            SkillsetsList.Add(tempSet);
            tempSet = new Skillset("CAD");
            tempSet.SkillsList.Add(new Skill("SolidWorks"));
            tempSet.SkillsList.Add(new Skill("AutoCAD"));
            tempSet.SkillsList.Add(new Skill("Siemens NX"));
            tempSet.SkillsList.Add(new Skill("Kompas-3D"));
            tempSet.SkillsList.Add(new Skill("Creo Parametrics"));
            tempSet.SkillsList.Add(new Skill("Pro Engineer"));
            tempSet.SkillsList.Add(new Skill("Autodesk Inventor"));
            tempSet.SkillsList.Add(new Skill("Catia"));
            SkillsetsList.Add(tempSet);
            tempSet = new Skillset("Programming");
            tempSet.SkillsList.Add(new Skill("Visual Studio"));
            tempSet.SkillsList.Add(new Skill("Android Studio"));   
            SkillsetsList.Add(tempSet);
            Employment tempEmployment = new Employment();
            tempEmployment.Title = "Mechanical Engineer";
            tempEmployment.Employer = "Class Engineering";
            tempEmployment.StartDate = DateTime.Now;
            tempEmployment.EndDate = DateTime.Now;
            tempEmployment.ExperiencesList.Add(new Experience("Solidworks", "Designed some cool stuff"));
            EmploymentsList.Add(tempEmployment);
            Education tempEducation = new Education();
            tempEducation.Institution = "ITMO University";
            tempEducation.WithHonors= true;
            tempEducation.Description = "Thesis title";
            tempEducation.Degree = "Bachelor degree";
            tempEducation.Program = "Mechatronics and Robotics";
            tempEducation.StartDate = DateTime.Now;
            tempEducation.EndDate = DateTime.Now;
            EducationsList.Add(tempEducation);
            ProffessionalAffiliation tempAffiliation = new ProffessionalAffiliation();
            tempAffiliation.Company = "Google";
            tempAffiliation.Date = DateTime.Now;
            tempAffiliation.Description = "Google Project Management Course Graduate";
            AffiliationsList.Add(tempAffiliation);
            Language tempLanguage = new Language();
            tempLanguage.LanguageName = "English";
            tempLanguage.Proficiency = "Professional (C1 - 105 TOEFL iBT)";
            LanguagesList.Add(tempLanguage);
            tempLanguage = new Language();
            tempLanguage.LanguageName = "Russian";
            tempLanguage.Proficiency = "Native"; 
            LanguagesList.Add(tempLanguage);
            Contact tempContact = new Contact();
            tempContact.ContactType = "Email";
            tempContact.ContactDescription = "Rob.kov96@gmail.com";
            ContactsList.Add(tempContact);
        }

        public ApplicantProfile(string name)
        {
            Name = name;
            InitializeDefaultParameters();
        }

        private void InitializeDefaultParameters()
        {
            ID = Int64.Parse(DateTime.Now.ToString("ddyyMMHHmmss"));
            TitlesList = new List<string>();
            AffiliationsList = new ObservableCollection<ProffessionalAffiliation>();
            EmploymentsList = new ObservableCollection<Employment>();
            EducationsList = new ObservableCollection<Education>();
            SkillsetsList = new ObservableCollection<Skillset>();
            ContactsList = new ObservableCollection<Contact>();
            LanguagesList = new ObservableCollection<Language>();
        }

    }
}
