using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI
{
    [Serializable]
    public class Employment
    {
        public string Employer { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //Item 1 is SkillTag, Item 2 is Experience Description
        public List<Experience> ExperiencesList { get; set; }
        //ResumeBuilder writes here experiences to be printed in application
        public List<string> RelevantExperience { get; set; }

        public Employment()
        {
            Employer = string.Empty;
            Title = string.Empty;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            ExperiencesList = new List<Experience>();
            RelevantExperience = new List<string>();
        }

        public Employment(string EmploymentInfo)
        {
            RelevantExperience = new List<string>();
            ExperiencesList = new List<Experience>();
            string[] parts = EmploymentInfo.Split(" | ");
            Employer = parts[0];
            Title = parts[1];
            StartDate = DateTime.Parse(parts[2]);
            EndDate = DateTime.Parse(parts[3]);
        }

        public static List<Employment> SortListOfEmployments(List<Employment> employments)
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

        public Employment Clone()
        {
            return this.MemberwiseClone() as Employment;
        }
    }
}
