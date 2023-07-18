using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI
{
    public class Education
    {
        public string Institution { get; set; }
        public string Degree { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool WithHonors { get; set; }
        public string Program { get; set; }
        public string Description { get; set; }

        public Education()
        {
            Institution= string.Empty;
            Degree= string.Empty;
            StartDate=DateTime.Now; 
            EndDate=DateTime.Now;
            WithHonors= false;
            Program= string.Empty;
            Description= string.Empty;
        }

        public Education(string institution, string degree, DateTime startDate, 
            DateTime endDate, bool withHonors, string program, string description)
        {
            Institution = institution;
            Degree = degree;
            StartDate = startDate;
            EndDate = endDate;
            WithHonors = withHonors;
            Program = program;
            Description = description;
        }
    }
}
