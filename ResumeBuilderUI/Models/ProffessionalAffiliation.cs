using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace ResumeBuilderUI.Models
{
    /// <summary>
    /// Class that stores any affiliation an Applicant had - courses, internships, competitions etc.
    /// </summary>
    public class ProffessionalAffiliation : ResumeElementBase
    {
        #region Fields and Properties
        private string _company;
        public string Company
        {
            get { return _company; }
            set { _company = value; OnPropertyChanged(nameof(Company)); }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description= value; OnPropertyChanged(nameof(Description)); }
        }
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { _date= value; OnPropertyChanged(nameof(Date));}
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Base Affiliation constructor
        /// </summary>
        public ProffessionalAffiliation()
        {
            Company= string.Empty;
            Description= string.Empty;
            Date= DateTime.Now;
        }
        /// <summary>
        /// Full Affiliation constructor
        /// </summary>
        public ProffessionalAffiliation(string company, string affiliation, DateTime dateOfAffiliation)
        {
            Company = company;
            Description = affiliation;
            Date = dateOfAffiliation;
        }

        /// <summary>
        /// ProfessionalAffiliation constructor that creates referenceless duplicate of provided ProfessionalAffiliation
        /// </summary>
        /// <param name="affiliation"></param>
        public ProffessionalAffiliation(ProffessionalAffiliation affiliation)
        {
            Company = affiliation.Company;
            Description = affiliation.Description;
            Date = affiliation.Date;
            IsSelected = affiliation.IsSelected;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Parses a string with a format "Year Company - Description to a ProfessionalAffiliation object
        /// </summary>
        /// <returns></returns>
        public static ProffessionalAffiliation Parse(string affiliationInString)
        {
            ProffessionalAffiliation parsedAffiliation = new ProffessionalAffiliation();
            parsedAffiliation.Company = affiliationInString.Substring(4, affiliationInString.IndexOf('-') - 5);
            parsedAffiliation.Description = affiliationInString.Substring((affiliationInString.IndexOf("-") + 1));
            parsedAffiliation.Date = DateTime.ParseExact(affiliationInString.Substring(0, 4), "yyyy", CultureInfo.InvariantCulture);
            parsedAffiliation.IsSelected = false;
            return parsedAffiliation;
        }

        /// <summary>
        /// Sorts List of ProfessionalAffiliations in reverse chronological order
        /// </summary>
        /// <returns>Sorted List of ProfessionalAffiliations</returns>
        public static List<ProffessionalAffiliation> Sort(List<ProffessionalAffiliation> affiliations)
        {
            affiliations.Sort((p, q) => p.Date.CompareTo(q.Date));
            affiliations.Reverse();
            return affiliations;
        }

        /// <summary>
        /// Sorts ObservableCollection of Professional Affiliations in reverse chronological order
        /// </summary>
        /// <returns>Sorted ObservableCollection of ProfessionalAffiliations</returns>
        public static ObservableCollection<ProffessionalAffiliation> Sort(ObservableCollection<ProffessionalAffiliation> affiliations)
        {
            List<ProffessionalAffiliation> tempAffiliations = affiliations.ToList();
            affiliations.Clear();
            foreach(ProffessionalAffiliation tempAffiliation in ProffessionalAffiliation.Sort(tempAffiliations))
            {
                affiliations.Add(tempAffiliation);
            }
            return affiliations;
        }

        /// <summary>
        /// Parses ProfessionalAffiliation object ToString() 
        /// </summary>
        /// <returns>String with format "Year Company - Description</returns>
        public override string ToString()
        {
            return Date.ToString("yyyy")+"  "+Company+" - "+Description;
        }
        #endregion

    }
}
