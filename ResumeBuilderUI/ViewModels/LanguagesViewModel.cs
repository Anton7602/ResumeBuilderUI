using ResumeBuilderUI.Models;

namespace ResumeBuilderUI.ViewModels
{
    class LanguagesViewModel : ViewModelBase
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
        //Edited Language - Language object that holds data before insertion into ActiveProfile.LanguagesList
        private Language editedLanguage = new Language { IsSelected = false };
        public Language EditedLanguage
        {
            get { return editedLanguage; }
            set
            {
                editedLanguage = value;
                OnPropertyChanged(nameof(EditedLanguage));
            }
        }
        //Commands
        public RelayCommand<object> EditNewLanguageCommand { get; private set; }
        public RelayCommand<object> SubmitNewLanguageCommand { get; private set; }
        #endregion

        #region Constructors
        public LanguagesViewModel()
        {
            EditNewLanguageCommand = new RelayCommand<object>(EditNewLanguage);
            SubmitNewLanguageCommand = new RelayCommand<object>(SubmitNewLanguage);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Makes visible a contactHolder in editMode to fill in parameters of new contact
        /// </summary>
        private void EditNewLanguage(object obj)
        {
            EditedLanguage.IsSelected = true;
        }
        /// <summary>
        /// Adds contact object, specified in editedContact into ActiveProfile and resets editedContact parameters
        /// </summary>
        private void SubmitNewLanguage(object obj)
        {
            ActiveProfile.LanguagesList.Insert(0, EditedLanguage);
            EditedLanguage = new Language { IsSelected = false };
        }
        #endregion
    }
}
