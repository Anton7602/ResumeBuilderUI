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
    }
}
