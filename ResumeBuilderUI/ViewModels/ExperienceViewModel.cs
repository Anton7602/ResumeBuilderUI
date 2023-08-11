using ResumeBuilderUI.Models;

namespace ResumeBuilderUI.ViewModels
{
    internal class ExperienceViewModel : ViewModelBase
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
        //Edited Employment - Employment object that holds data before insertion into ActiveProfile.EmploymentList
        private Employment _editedEmployment = new Employment();
        public Employment EditedEmployment
        {
            get { return _editedEmployment; }
            set
            {
                _editedEmployment = value;
                OnPropertyChanged(nameof(EditedEmployment));
            }
        }
        //Commands
        public RelayCommand<object> EditNewEmploymentCommand { get; private set; }
        public RelayCommand<object> SubmitNewEmploymentCommand { get; private set; }
        #endregion

        #region Constructors
        public ExperienceViewModel()
        {
            EditNewEmploymentCommand = new RelayCommand<object>(EditNewEmployment);
            SubmitNewEmploymentCommand = new RelayCommand<object>(SubmitNewEmployment);
        }
        #endregion

        #region Private Methods
        private void EditNewEmployment(object obj)
        {
            EditedEmployment.IsSelected = true;
        }
        /// <summary>
        /// Adds contact object, specified in editedContact into ActiveProfile and resets editedContact parameters
        /// </summary>
        private void SubmitNewEmployment(object obj)
        {
            ActiveProfile.EmploymentsList.Insert(0, EditedEmployment);
            EditedEmployment = new Employment { IsSelected = false };
        }
        #endregion
    }
}
