using ResumeBuilderUI.Models;

namespace ResumeBuilderUI.ViewModels
{
    class EducationViewModel : ViewModelBase
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
        //Edited Education - Education object that holds data before insertion into ActiveProfile.EducationList
        private Education _editedEducation = new Education();
        public Education EditedEducation
        {
            get { return _editedEducation; }
            set
            {
                _editedEducation = value;
                OnPropertyChanged(nameof(EditedEducation));
            }
        }
        //Commands
        public RelayCommand<object> EditNewEducationCommand { get; private set; }
        public RelayCommand<object> SubmitNewEducationCommand { get; private set; }
        #endregion

        #region Constructors
        public EducationViewModel()
        {
            EditNewEducationCommand = new RelayCommand<object>(EditNewEducation);
            SubmitNewEducationCommand = new RelayCommand<object>(SubmitNewEducation);
        }
        #endregion

        #region Private Methods
        private void EditNewEducation(object obj)
        {
            EditedEducation.IsSelected = true;
        }
        /// <summary>
        /// Adds contact object, specified in editedContact into ActiveProfile and resets editedContact parameters
        /// </summary>
        private void SubmitNewEducation(object obj)
        {
            ActiveProfile.EducationsList.Insert(0, EditedEducation);
            EditedEducation = new Education { IsSelected = false };
        }
        #endregion

    }
}
