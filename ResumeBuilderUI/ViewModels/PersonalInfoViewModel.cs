using ResumeBuilderUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI.ViewModels
{
    internal class PersonalInfoViewModel : ViewModelBase
    {
        private ApplicantProfile activeProfile;
        public ApplicantProfile ActiveProfile
        {
            get { return activeProfile; }
            set
            {
                activeProfile= value;
                App.activeProfile = activeProfile;
                OnPropertyChanged("ActiveProfile");
            }
        }
        public PersonalInfoViewModel()
        {
            activeProfile = App.activeProfile;
        }
    }
}
