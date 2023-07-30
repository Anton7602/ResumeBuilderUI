using ResumeBuilderUI.Models;

namespace ResumeBuilderUI.ViewModels
{
    internal class PersonalInfoViewModel : ViewModelBase
    {
        private ApplicantProfile activeProfile = App.ActiveProfile;
        public ApplicantProfile ActiveProfile
        {
            get { return activeProfile; }
            set
            {
                activeProfile= value;
                App.ActiveProfile = activeProfile;
                OnPropertyChanged("ActiveProfile");
            }
        }
        public PersonalInfoViewModel()
        {

        }
    }
}
