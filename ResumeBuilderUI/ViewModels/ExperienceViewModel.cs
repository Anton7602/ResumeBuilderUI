using ResumeBuilderUI.Models;

namespace ResumeBuilderUI.ViewModels
{
    internal class ExperienceViewModel : ViewModelBase
    {
        private ApplicantProfile activeProfile = App.ActiveProfile;
        public ApplicantProfile ActiveProfile
        {
            get { return activeProfile; }
            set
            {
                activeProfile = value;
                App.ActiveProfile = activeProfile;
                OnPropertyChanged(nameof(ActiveProfile));
            }
        }

        public ExperienceViewModel()
        {

        }
    }
}
