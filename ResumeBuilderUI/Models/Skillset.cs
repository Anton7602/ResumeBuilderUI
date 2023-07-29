using ResumeBuilderUI.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace ResumeBuilderUI.Models
{
    public class Skillset
    {
        public string MainSkill { get; set; }
        public ObservableCollection<Skill> SkillsList { get; set; }
        public bool IsSelected { get; set; }
        public RelayCommand<object> AddSkillCommand { get; private set; }

        public Skillset()
        {
            MainSkill = string.Empty;
            SkillsList = new ObservableCollection<Skill>();
            AddSkillCommand = new RelayCommand<object>(InsertSkillToSkillList);
            IsSelected= false;
        }

        public Skillset(string mainSkill)
        {
            MainSkill= mainSkill;
            SkillsList = new ObservableCollection<Skill>();
            AddSkillCommand = new RelayCommand<object>(InsertSkillToSkillList);
            IsSelected= false;
        }

        public Skillset(Skillset skillset)
        {
            MainSkill = skillset.MainSkill;
            SkillsList = skillset.SkillsList;
            AddSkillCommand = new RelayCommand<object>(InsertSkillToSkillList);
            IsSelected = skillset.IsSelected;
        }

        public static List<Skillset> Sort(List<Skillset> skillsets)
        {
            skillsets.Sort((p, q) => p.SkillsList.Count.CompareTo(q.SkillsList.Count));
            skillsets.Reverse();
            return skillsets;
        }

        private void InsertSkillToSkillList(object obj)
        {
            SkillsList.Insert(0, new Skill(obj as string));
        }
    }
}
