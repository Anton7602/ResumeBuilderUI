
namespace ResumeBuilderUI.Models
{
    /// <summary>
    /// Class that represents a singular skill, gained by employment or education
    /// </summary>
    public class Skill
    {
        #region Fields and Properties
        public string SkillName { get; set; }
        public bool IsSelected { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Base Skill Constructor
        /// </summary>
        /// <param name="skillName"></param>
        public Skill(string skillName)
        {
            SkillName = skillName;
            IsSelected = false;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Override of Equals method with a new definition of Skill equality
        /// </summary>
        /// <returns>True or False, depending on objects equality</returns>
        public override bool Equals(object? obj)
        {
            return SkillName.Equals((obj as Skill).SkillName);
        }
        #endregion
    }
}
