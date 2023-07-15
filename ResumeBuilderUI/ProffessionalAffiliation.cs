using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI
{
    public class ProffessionalAffiliation
    {
        public string Company { get; set; }
        public string Affiliation { get; set; }
        public DateTime DateOfAffiliation { get; set; }

        public ProffessionalAffiliation()
        {
            Company= string.Empty;
            Affiliation= string.Empty;
            DateOfAffiliation= DateTime.MinValue;
        }

        public ProffessionalAffiliation(string company, string affiliation, DateTime dateOfAffiliation)
        {
            Company = company;
            Affiliation = affiliation;
            DateOfAffiliation = dateOfAffiliation;
        }

        public static List<ProffessionalAffiliation> SortListOfAffiliations(List<ProffessionalAffiliation> affiliations)
        {
            affiliations.Sort((p, q) => p.DateOfAffiliation.CompareTo(q.DateOfAffiliation));
            affiliations.Reverse();
            return affiliations;
        }

        public static ProffessionalAffiliation Parse(string affiliationInString)
        {
            ProffessionalAffiliation parsedAffiliation = new ProffessionalAffiliation();
            parsedAffiliation.Company = affiliationInString.Substring(4, affiliationInString.IndexOf('-') - 5);
            parsedAffiliation.Affiliation = affiliationInString.Substring((affiliationInString.IndexOf("-") + 1));
            parsedAffiliation.DateOfAffiliation = DateTime.ParseExact(affiliationInString.Substring(0, 4), "yyyy", CultureInfo.InvariantCulture);
            return parsedAffiliation;
        }

        public override string ToString()
        {
            return DateOfAffiliation.ToString("yyyy")+"  "+Company+" - "+Affiliation;
        }
    }
}
