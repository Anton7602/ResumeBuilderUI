using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI.Models
{
    [Serializable]
    public class ApplicantProfile
    {
        public enum Language {English, Russian}
        public long ID { get; set; }
        public string? Name { get; set; }
        public List<string> TitlesList { get; set; }
        public List<Employment> EmploymentsList { get; set; }
        public ObservableCollection<Skillset> SkillsetsList { get; set; }
        public List<Education> EducationsList { get; set; }
        public List<ProffessionalAffiliation> AffiliationsList { get; set; }
        public Dictionary<string, string> LanguagesList { get; set; }
        public Dictionary<string, string> ContactsList { get; set; }
        public Language DefaultLanguage { get; set; }

        public ApplicantProfile()
        {
            Name = "Placeholder";
            InitializeDefaultParameters();
            TitlesList.Add("Cool Fella");
            Skillset tempSet = new Skillset("CAD");
            tempSet.SkillsList.Add("SolidWorks");
            tempSet.SkillsList.Add("AutoCAD");
            tempSet.SkillsList.Add("Siemens NX");
            tempSet.SkillsList.Add("Kompas-3D");
            tempSet.SkillsList.Add("Creo Parametrics");
            tempSet.SkillsList.Add("Pro Engineer");
            tempSet.SkillsList.Add("Autodesk Inventor");
            tempSet.SkillsList.Add("Catia");
            SkillsetsList.Add(tempSet);
            tempSet = new Skillset("Programming");
            tempSet.SkillsList.Add("Visual Studio");
            tempSet.SkillsList.Add("Android Studio");
            SkillsetsList.Add(tempSet);
            tempSet = new Skillset("CAD");
            tempSet.SkillsList.Add("SolidWorks");
            tempSet.SkillsList.Add("AutoCAD");
            tempSet.SkillsList.Add("Siemens NX");
            tempSet.SkillsList.Add("Kompas-3D");
            tempSet.SkillsList.Add("Creo Parametrics");
            tempSet.SkillsList.Add("Pro Engineer");
            tempSet.SkillsList.Add("Autodesk Inventor");
            tempSet.SkillsList.Add("Catia");
            SkillsetsList.Add(tempSet);
            tempSet = new Skillset("Programming");
            tempSet.SkillsList.Add("Visual Studio");
            tempSet.SkillsList.Add("Android Studio");
            SkillsetsList.Add(tempSet);
            tempSet = new Skillset("CAD");
            tempSet.SkillsList.Add("SolidWorks");
            tempSet.SkillsList.Add("AutoCAD");
            tempSet.SkillsList.Add("Siemens NX");
            tempSet.SkillsList.Add("Kompas-3D");
            tempSet.SkillsList.Add("Creo Parametrics");
            tempSet.SkillsList.Add("Pro Engineer");
            tempSet.SkillsList.Add("Autodesk Inventor");
            tempSet.SkillsList.Add("Catia");
            SkillsetsList.Add(tempSet);
            tempSet = new Skillset("Programming");
            tempSet.SkillsList.Add("Visual Studio");
            tempSet.SkillsList.Add("Android Studio");
            SkillsetsList.Add(tempSet);
        }

        public ApplicantProfile(string name)
        {
            Name = name;
            InitializeDefaultParameters();
        }

        private void InitializeDefaultParameters()
        {
            ID = Int64.Parse(DateTime.Now.ToString("ddyyMMHHmmss"));
            DefaultLanguage = Language.English;
            TitlesList = new List<string>();
            AffiliationsList = new List<ProffessionalAffiliation>();
            LanguagesList = new Dictionary<string, string>();
            EmploymentsList = new List<Employment>();
            SkillsetsList = new ObservableCollection<Skillset>();
            ContactsList = new Dictionary<string, string>();
        }

    }
}
