using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace ResumeBuilderUI.Models
{
    public class ProffessionalAffiliation : INotifyPropertyChanged
    {
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
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged(nameof(IsSelected)); }
        }

        public ProffessionalAffiliation()
        {
            Company= string.Empty;
            Description= string.Empty;
            Date= DateTime.Now;
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

        public static List<ProffessionalAffiliation> Sort(List<ProffessionalAffiliation> affiliations)
        {
            affiliations.Sort((p, q) => p.Date.CompareTo(q.Date));
            affiliations.Reverse();
            return affiliations;
        }

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

        public override string ToString()
        {
            return Date.ToString("yyyy")+"  "+Company+" - "+Description;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName = null));
        }
    }
}
