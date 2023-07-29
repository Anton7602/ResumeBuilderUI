using System;
using System.Collections.Generic;
using System.Globalization;

namespace ResumeBuilderUI.Models
{
    public class ProffessionalAffiliation
    {
        public string Company { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsSelected { get; set; }

        public ProffessionalAffiliation()
        {
            Company= string.Empty;
            Description= string.Empty;
            Date= DateTime.MinValue;
            IsSelected= false;
        }

        public ProffessionalAffiliation(string company, string affiliation, DateTime dateOfAffiliation)
        {
            Company = company;
            Description = affiliation;
            Date = dateOfAffiliation;
            IsSelected = false;
        }

        public ProffessionalAffiliation(ProffessionalAffiliation affiliation)
        {
            Company = affiliation.Company;
            Description = affiliation.Description;
            Date = affiliation.Date;
            IsSelected = affiliation.IsSelected;
        }

        public static List<ProffessionalAffiliation> SortListOfAffiliations(List<ProffessionalAffiliation> affiliations)
        {
            affiliations.Sort((p, q) => p.Date.CompareTo(q.Date));
            affiliations.Reverse();
            return affiliations;
        }

        public static ProffessionalAffiliation Parse(string affiliationInString)
        {
            ProffessionalAffiliation parsedAffiliation = new ProffessionalAffiliation();
            parsedAffiliation.Company = affiliationInString.Substring(4, affiliationInString.IndexOf('-') - 5);
            parsedAffiliation.Description = affiliationInString.Substring((affiliationInString.IndexOf("-") + 1));
            parsedAffiliation.Date = DateTime.ParseExact(affiliationInString.Substring(0, 4), "yyyy", CultureInfo.InvariantCulture);
            parsedAffiliation.IsSelected = false;
            return parsedAffiliation;
        }

        public override string ToString()
        {
            return Date.ToString("yyyy")+"  "+Company+" - "+Description;
        }
    }
}
