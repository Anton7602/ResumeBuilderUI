using ResumeBuilderUI.Models;

namespace ResumeBuilderUI.ViewModels
{
    internal class SkillsViewModel : ViewModelBase
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
