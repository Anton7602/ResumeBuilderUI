using ResumeBuilderUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI.ViewModels
{
    class LanguagesViewModel : ViewModelBase
    {
        private ApplicantProfile activeProfile = App.activeProfile;
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
