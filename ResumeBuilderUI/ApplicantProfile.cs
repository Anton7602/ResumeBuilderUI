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
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public Language DefaultLanguage { get; set; }
        public List<string> affiliationsList { get; set; }
        public List<string> languagesList { get; set; }
        public List<Employment> employmentsList { get; set; }
        public Dictionary<string, string> skillsList { get; set; }
        public Dictionary<string, string> contactsList { get; set; }

        public ApplicantProfile()
        {
            Name = "Placeholder";
            Surname = "Placeholder";
            DefaultLanguage = Language.English;
            affiliationsList = new List<string>();
            languagesList = new List<string>();
            employmentsList = new List<Employment>();
            skillsList = new Dictionary<string, string>();
            contactsList = new Dictionary<string, string>();
        }

        public ApplicantProfile(string name, string surname)
        {
            Name = name;
            Surname = surname;
            DefaultLanguage = Language.English;
            affiliationsList= new List<string>();
            languagesList= new List<string>();
            employmentsList= new List<Employment>();
            skillsList= new Dictionary<string, string>();
            contactsList= new Dictionary<string, string>();
        }

    }
}
