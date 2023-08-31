using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ResumeBuilderUI.Models
{
    /// <summary>
    /// Class that stores all Applicant information in the App
    /// </summary>
    [Serializable]
    public class ApplicantProfile
    {
        #region Fields and Properties
        public long ID { get; set; }
        public string? Name { get; set; }
        public List<string> TitlesList { get; set; }
        public string Summary { get; set; }
        public ObservableCollection<Employment> EmploymentsList { get; set; }
        public ObservableCollection<Skillset> SkillsetsList { get; set; }
        public ObservableCollection<Education> EducationsList { get; set; }
        public ObservableCollection<ProffessionalAffiliation> AffiliationsList { get; set; }
        public ObservableCollection<Language> LanguagesList { get; set; }
        public ObservableCollection<Contact> ContactsList { get; set; }
        public string AvatarImagePath { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Base ApplicantProfile Constructor
        /// </summary>
        public ApplicantProfile()
        {
            Name = "Placeholder";
            ID = Int64.Parse(DateTime.Now.ToString("ddyyMMHHmmss"));
            TitlesList = new List<string>();
            TitlesList.Add(string.Empty);
            AffiliationsList = new ObservableCollection<ProffessionalAffiliation>();
            EmploymentsList = new ObservableCollection<Employment>();
            EducationsList = new ObservableCollection<Education>();
            SkillsetsList = new ObservableCollection<Skillset>();
            ContactsList = new ObservableCollection<Contact>();
            LanguagesList = new ObservableCollection<Language>();
            AvatarImagePath  = string.Empty;
        }

        /// <summary>
        /// ApplicantProfile constructor with name setting
        /// </summary>
        public ApplicantProfile(string name) : this()
        {
            Name =name;
        }
        #endregion
    }
}
