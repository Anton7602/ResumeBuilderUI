using ResumeBuilderUI.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace ResumeBuilderUI.Models
{
    /// <summary>
    /// Class that represents a skill category, with embeded skill list
    /// </summary>
    public class Skillset: ResumeElementBase
    {
        #region Fields and Properties
        private string _mainSkill = string.Empty;
        public string MainSkill
        {
            get { return _mainSkill; }
            set
            {
                _mainSkill = value;
                OnPropertyChanged(nameof(MainSkill));
            }
        }
        public ObservableCollection<Skill> SkillsList { get; set; }
        public RelayCommand<object> AddSkillCommand { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Base Skillset Constructor
        /// </summary>
        public Skillset()
        {
            SkillsList = new ObservableCollection<Skill>();
            AddSkillCommand = new RelayCommand<object>(InsertSkillToSkillList);
        }

        /// <summary>
        /// Skillset constructor with Main Skill setting
        /// </summary>
        /// <param name="mainSkill"></param>
        public Skillset(string mainSkill) : this()
        {
            MainSkill= mainSkill;
        }
        /// <summary>
        /// Skillset constructor that create referenceless duplicate of provided Skillset
        /// </summary>
        public Skillset(Skillset skillset)
        {
            MainSkill = skillset.MainSkill;
            SkillsList = skillset.SkillsList;
            AddSkillCommand = new RelayCommand<object>(InsertSkillToSkillList);
            IsSelected = skillset.IsSelected;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sorts provided Skillset by amount of skill in skill lists from highest to lowest
        /// </summary>
        /// <returns>Sorted Skillsets List</returns>
        public static List<Skillset> Sort(List<Skillset> skillsets)
        {
            skillsets.Sort((p, q) => p.SkillsList.Count.CompareTo(q.SkillsList.Count));
            skillsets.Reverse();
            return skillsets;
        }
        #endregion

        #region Private Methods
        private void InsertSkillToSkillList(object obj)
        {
            SkillsList.Insert(0, new Skill(obj as string));
        }
        #endregion
    }
}
