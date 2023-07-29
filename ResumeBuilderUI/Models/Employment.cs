using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI.Models
{
    [Serializable]
    public class Employment
    {
        public string Employer { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Experience> ExperiencesList { get; set; }
        public bool IsSelected { get; set; }
        //ResumeBuilder writes here experiences to be printed in application

        public Employment()
        {
            Employer = string.Empty;
            Title = string.Empty;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            IsSelected = false;
            ExperiencesList = new List<Experience>();
        }

        public Employment(string EmploymentInfo)
        {
            ExperiencesList = new List<Experience>();
            string[] parts = EmploymentInfo.Split(" | ");
            Employer = parts[0];
            Title = parts[1];
            StartDate = DateTime.Parse(parts[2]);
            EndDate = DateTime.Parse(parts[3]);
            IsSelected = false;
        }

        public Employment(Employment employment)
        {
            Employer = employment.Employer;
            Title = employment.Title;
            StartDate = employment.StartDate;
            EndDate = employment.EndDate;
            IsSelected = false;
            ExperiencesList = new List<Experience>(employment.ExperiencesList);
        }

        public static List<Employment> Sort(List<Employment> employments)
        {
            employments.Sort((p, q) => p.EndDate.CompareTo(q.EndDate));
            employments.Reverse();
            return employments;
        }

        public override string ToString()
        {
            return (Employer + " | " + Title + " | " + StartDate.ToString("Y") + " | " + EndDate.ToString("Y"));
        }

        public string ToString(string format)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(format);
            return (Employer + " | " + Title + " | " + StartDate.ToString("Y") + " | " + EndDate.ToString("Y"));
        }
    }
}
