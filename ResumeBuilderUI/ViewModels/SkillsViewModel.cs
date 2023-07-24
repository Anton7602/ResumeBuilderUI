using ResumeBuilderUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI.ViewModels
{
    internal class SkillsViewModel : ViewModelBase
    {
        public SkillsViewModel()
        {
            ActiveProfile = App.activeProfile;
            ActiveSkills = activeProfile.SkillsetsList;
        }

        private ApplicantProfile activeProfile;
        public ApplicantProfile ActiveProfile
        {
            get { return activeProfile; }
            set
            {
                activeProfile = value;
                App.activeProfile = activeProfile;
                OnPropertyChanged(nameof(ActiveProfile));
            }
        }

        private ObservableCollection<Skillset> activeSkills;
        public ObservableCollection<Skillset> ActiveSkills
        {
            get { return activeSkills; }
            set
            {
                activeSkills = value;
                OnPropertyChanged(nameof(ActiveSkills));
            }
        }

        private RelayCommand addSkill;
        public RelayCommand AddSkill
        {
            get
            {
                return addSkill ??
                  (addSkill = new RelayCommand(obj =>
                  {
                      activeSkills.Add(new Skillset("TestSkill"));

                      OnPropertyChanged(nameof(ActiveSkills));
                  }));
            }
        }
    }
}
