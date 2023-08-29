using System;
using System.Collections.Generic;

namespace ResumeBuilderUI.Models
{
    /// <summary>
    /// Class that represents individual achievements and experiences within an Employment
    /// </summary>
    [Serializable]
    public class Experience: ResumeElementBase
    {
        #region Fields and Properties
        public enum Priorities {Low, Medium, High }
        private string _tag = string.Empty;
        public string Tag
        {
            get { return _tag; }
            set
            {
                _tag = value;
                OnPropertyChanged(nameof(Tag));
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
        private Priorities _priority = Priorities.Low;
        public Priorities Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }
        #endregion

        #region Constructors
        public Experience()
        {
            Tag= string.Empty;
            Description= string.Empty;
            Priority = Priorities.Low;
        }

        public Experience(string experienceTag, string experienceDescription, Priorities experiencePriority)
        {
            Tag = experienceTag;
            Description = experienceDescription;
            Priority = experiencePriority;
        }

        public Experience(string experienceTag, string experienceDescription)
        {
            Tag = experienceTag;
            Description = experienceDescription;
            Priority = Priorities.Low;
        }

        public Experience(Experience experience)
        {
            Tag = experience.Tag;
            Description = experience.Description;
            Priority= experience.Priority;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sorts list of Experiences by Priority parameter
        /// </summary>
        /// <returns>Sorted list of Experiences</returns>
        public static List<Experience> Sort(List<Experience> experiences)
        {
            experiences.Sort((p, q) => p.Priority.CompareTo(q.Priority));
            experiences.Reverse();
            return experiences;
        }
        #endregion
    }
}
