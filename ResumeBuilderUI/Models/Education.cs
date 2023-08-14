using System;
using System.Collections.Generic;

namespace ResumeBuilderUI.Models
{
    /// <summary>
    /// Class that stores info about Education instances of an Applicant
    /// </summary>
    public class Education: ResumeElementBase
    {
        #region Fields and Parameters
        private string _institution = string.Empty;
        public string Institution
        {
            get { return _institution; }
            set
            {
                _institution= value;
                OnPropertyChanged(nameof(Institution));
            }
        }
        private string _degree = string.Empty;
        public string Degree
        {
            get { return _degree; }
            set
            {
                _degree= value;
                OnPropertyChanged(nameof(Degree));
            }
        }
        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate= value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate= value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        private bool _withHonors = false;
        public bool WithHonors
        {
            get { return _withHonors;}
            set
            {
                _withHonors= value;
                OnPropertyChanged(nameof(WithHonors));
            }
        }
        private string _program = string.Empty;
        public string Program
        {
            get { return _program; }
            set
            {
                _program= value;
                OnPropertyChanged(nameof(Program));
            }
        }
        private string _description = string.Empty;
        public string Description
        {
            get { return _description; }
            set
            {
                _description= value;
                OnPropertyChanged(nameof(Description));
            }
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Base Education Constructor
        /// </summary>
        public Education() { }

        /// <summary>
        /// Adcanced Education Constructor with all parameters 
        /// </summary>
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
            IsSelected= false;
        }

        /// <summary>
        /// Sorts given list of Employments in reverse chronological order by Employment EndDate
        /// </summary>
        /// <returns>Sorted list of Employments</returns>
        public static List<Education> Sort(List<Education> educations)
        {
            educations.Sort((p, q) => p.EndDate.CompareTo(q.EndDate));
            educations.Reverse();
            return educations;
        }
        #endregion
    }
}
