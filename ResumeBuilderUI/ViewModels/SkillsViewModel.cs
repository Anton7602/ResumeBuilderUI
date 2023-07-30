using ResumeBuilderUI.Models;

namespace ResumeBuilderUI.ViewModels
{
    /// <summary>
    /// ViewModel for SkillsView
    /// </summary>
    internal class SkillsViewModel : ViewModelBase
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
