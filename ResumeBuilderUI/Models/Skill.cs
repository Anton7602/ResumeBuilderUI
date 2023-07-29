
namespace ResumeBuilderUI.Models
{
    public class Skill
    {
        public string SkillName { get; set; }
        public bool IsSelected { get; set; }

        public Skill(string skillName)
        {
            SkillName = skillName;
            IsSelected = false;
        }
    }
}
