using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI
{
    [Serializable]
    public class Experience
    {
        public enum Priorities {low, medium, high }
        public string Tag { get; set; }
        public string Description { get; set; }
        public Priorities Priority { get; set; }

        public Experience()
        {
            Tag= string.Empty;
            Description= string.Empty;
            Priority = Priorities.low;
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
            Priority = Priorities.low;
        }

        public Experience(Experience experience)
        {
            Tag = experience.Tag;
            Description = experience.Description;
            Priority= experience.Priority;
        }

        public static List<Experience> Sort(List<Experience> experiences)
        {
            experiences.Sort((p, q) => p.Priority.CompareTo(q.Priority));
            experiences.Reverse();
            return experiences;
        }
    }
}
