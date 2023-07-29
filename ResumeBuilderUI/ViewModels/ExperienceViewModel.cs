using ResumeBuilderUI.Models;

namespace ResumeBuilderUI.ViewModels
{
    internal class ExperienceViewModel : ViewModelBase
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

        public ExperienceViewModel()
        {

        }
    }
}
