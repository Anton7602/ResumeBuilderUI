using System;
using System.Collections.Generic;
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
        public List<Employment> employmentsList { get; set; }
        public List<Skillset> skillsetsList { get; set; }
        public List<Education> educationsList { get; set; }
        public List<ProffessionalAffiliation> affiliationsList { get; set; }
        public Dictionary<string, string> languagesList { get; set; }
        public Dictionary<string, string> contactsList { get; set; }
        public Language DefaultLanguage { get; set; }
        public List<bool> expanderStates { get; set; }

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
            skillsetsList.Add(tempSet);
            tempSet = new Skillset("Programming");
            tempSet.SkillsList.Add("Visual Studio");
            tempSet.SkillsList.Add("Android Studio");
            skillsetsList.Add(tempSet);
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
            affiliationsList = new List<ProffessionalAffiliation>();
            languagesList = new Dictionary<string, string>();
            employmentsList = new List<Employment>();
            skillsetsList = new List<Skillset>();
            contactsList = new Dictionary<string, string>();
            expanderStates = new List<bool> { true, false, false, false };
        }

    }
}
