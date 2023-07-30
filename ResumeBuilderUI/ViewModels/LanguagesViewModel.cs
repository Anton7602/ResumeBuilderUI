using ResumeBuilderUI.Models;

namespace ResumeBuilderUI.ViewModels
{
    class LanguagesViewModel : ViewModelBase
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
    }
}
