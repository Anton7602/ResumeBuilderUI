using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI
{
    public class Employment
    {
        public string Employer { get; private set; }
        public string Title { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        //Item 1 is SkillTag, Item 2 is Experience Description
        public List<(string, string)> Experience { get; set; }
        //ResumeBuilder writes here experiences to be printed in application
        public List<string> RelevantExperience { get; set; }

        public Employment(string EmploymentInfo)
        {
            RelevantExperience = new List<string>();
            Experience = new List<(string, string)>();
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
    }
}
