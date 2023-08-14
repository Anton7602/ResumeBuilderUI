using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ResumeBuilderUI.Models
{
    /// <summary>
    /// Class that stores information about employment instances of an Applicant
    /// </summary>
    [Serializable]
    public class Employment: ResumeElementBase
    {
        #region Fields and Properties
        private string _employer = string.Empty;
        public string Employer
        {
            get { return _employer; }
            set
            {
                _employer = value;
                OnPropertyChanged(nameof(Employer));
            }
        }
        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        public ObservableCollection<Experience> ExperiencesList { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Base Employment Constructor
        /// </summary>
        public Employment()
        {
            ExperiencesList = new ObservableCollection<Experience>();
        }

        /// <summary>
        /// Employment constructor that creates Employment from provided string.  
        /// String must have format "Employment | Title | StartDate | EndDate" 
        /// </summary>
        public Employment(string EmploymentInfo)
        {
            ExperiencesList = new ObservableCollection<Experience>();
            string[] parts = EmploymentInfo.Split(" | ");
            Employer = parts[0];
            Title = parts[1];
            StartDate = DateTime.Parse(parts[2]);
            EndDate = DateTime.Parse(parts[3]);
            IsSelected = false;
        }
        /// <summary>
        /// Employment constructor that creates a referenceless duplicate of provided Employment. 
        /// </summary>
        public Employment(Employment employment)
        {
            Employer = employment.Employer;
            Title = employment.Title;
            StartDate = employment.StartDate;
            EndDate = employment.EndDate;
            IsSelected = false;
            ExperiencesList = new ObservableCollection<Experience>(employment.ExperiencesList);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sorts given list of Employments in reverse chronological order by Employment EndDate
        /// </summary>
        /// <returns>Sorted list of Employments</returns>
        public static List<Employment> Sort(List<Employment> employments)
        {
            employments.Sort((p, q) => p.EndDate.CompareTo(q.EndDate));
            employments.Reverse();
            return employments;
        }

        /// <summary>
        /// Parses Employment object to string
        /// </summary>
        /// <returns>Employment as a string in format: "Employer | Title | StartDate(year month) | EndDate(year month)"</returns>
        public override string ToString()
        {
            return (Employer + " | " + Title + " | " + StartDate.ToString("Y") + " | " + EndDate.ToString("Y"));
        }

        /// <summary>
        /// Parses Employment object to string with start/end dates written according to given cultureInfo type
        /// </summary>
        /// <returns>Employment as a string in format: "Employer | Title | StartDate(year month) | EndDate(year month)"</returns>
        public string ToString(string format)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(format);
            return (Employer + " | " + Title + " | " + StartDate.ToString("Y") + " | " + EndDate.ToString("Y"));
        }
        #endregion
    }
}
