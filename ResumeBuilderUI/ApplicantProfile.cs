using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI
{
    [Serializable]
    public class ApplicantProfile
    {
        public enum Language {English, Russian}
        public long ID { get; set; }
        public string? Name { get; set; }
        public Language DefaultLanguage { get; set; }
        public List<string> TitlesList { get; set; }
        public List<ProffessionalAffiliation> affiliationsList { get; set; }
        public List<string> languagesList { get; set; }
        public List<Employment> employmentsList { get; set; }
        public List<Skillset> skillsetsList { get; set; }
        public Dictionary<string, string> contactsList { get; set; }
        public List<bool> expanderStates { get; set; }

        public ApplicantProfile()
        {
            Name = "Placeholder";
            InitializeDefaultParameters();
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
            languagesList = new List<string>();
            employmentsList = new List<Employment>();
            skillsetsList = new List<Skillset>();
            contactsList = new Dictionary<string, string>();
            expanderStates = new List<bool> { true, false, false, false };
        }

    }
}
