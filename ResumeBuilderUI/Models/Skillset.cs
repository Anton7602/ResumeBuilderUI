using ResumeBuilderUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI.Models
{
    public class Skillset
    {
        public string MainSkill { get; set; }
        public ObservableCollection<string> SkillsList { get; set; }
        public RelayCommand<object> AddSkillCommand { get; private set; }

        public Skillset()
        {
            MainSkill = string.Empty;
            SkillsList = new ObservableCollection<string>();
            AddSkillCommand = new RelayCommand<object>(InsertSkillToSkillList);
        }

        public Skillset(string mainSkill)
        {
            MainSkill= mainSkill;
            SkillsList = new ObservableCollection<string>();
            AddSkillCommand = new RelayCommand<object>(InsertSkillToSkillList);
        }

        public Skillset(Skillset skillset)
        {
            MainSkill = skillset.MainSkill;
            SkillsList = skillset.SkillsList;
            AddSkillCommand = new RelayCommand<object>(InsertSkillToSkillList);
        }

        public static List<Skillset> Sort(List<Skillset> skillsets)
        {
            skillsets.Sort((p, q) => p.SkillsList.Count.CompareTo(q.SkillsList.Count));
            skillsets.Reverse();
            return skillsets;
        }

        private void InsertSkillToSkillList(object obj)
        {
            SkillsList.Insert(0, (obj as string));
        }
    }
}
