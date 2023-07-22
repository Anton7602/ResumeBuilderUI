using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI.Models
{
    public class Skillset
    {
        public string MainSkill { get; set; }
        public List<string> SkillsList { get; set; }

        public Skillset()
        {
            MainSkill = string.Empty;
            SkillsList = new List<string>();
        }

        public Skillset(string mainSkill)
        {
            MainSkill= mainSkill;
            SkillsList = new List<string>();
        }

        public Skillset(Skillset skillset)
        {
            MainSkill = skillset.MainSkill;
            SkillsList = skillset.SkillsList;
        }

        public static List<Skillset> Sort(List<Skillset> skillsets)
        {
            skillsets.Sort((p, q) => p.SkillsList.Count.CompareTo(q.SkillsList.Count));
            skillsets.Reverse();
            return skillsets;
        }
    }
}
