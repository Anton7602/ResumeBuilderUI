using ResumeBuilderUI.Models;

namespace ResumeBuilderUI.ViewModels
{
    /// <summary>
    /// ViewModel for SkillsView
    /// </summary>
    internal class SkillsViewModel : ViewModelBase
    {
        #region Fields and Properties
        //ActiveProfile - reference to an App.ActiveProfile (for binding purposes)
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
        //Edited Skillset - Skillset object that holds data before insertion into ActiveProfile.SkillsetsList
        private Skillset _editedSkillset = new Skillset();
        public Skillset EditedSkillset
        {
            get { return _editedSkillset; }
            set
            {
                _editedSkillset= value;
                OnPropertyChanged(nameof(EditedSkillset));
            }
        }
        //Commands
        public RelayCommand<object> EditNewSkillsetCommand { get; private set; }
        public RelayCommand<object> SubmitNewSkillsetCommand { get; private set; }
        #endregion

        #region Constructors
        public SkillsViewModel()
        {
            EditNewSkillsetCommand = new RelayCommand<object>(EditNewSkillset);
            SubmitNewSkillsetCommand = new RelayCommand<object>(SubmitNewSkillset);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Makes visible a contactHolder in editMode to fill in parameters of new contact
        /// </summary>
        private void EditNewSkillset(object obj)
        {
            EditedSkillset.IsSelected = true;
        }
        /// <summary>
        /// Adds contact object, specified in editedContact into ActiveProfile and resets editedContact parameters
        /// </summary>
        private void SubmitNewSkillset(object obj)
        {
            EditedSkillset.MainSkill = (string)obj;
            ActiveProfile.SkillsetsList.Insert(0, EditedSkillset);
            EditedSkillset = new Skillset {};
        }
        #endregion


    }
}
