using ResumeBuilderUI.Models;
using ResumeBuilderUI.UserControls;
using System;

namespace ResumeBuilderUI.ViewModels
{
    class AffiliationsViewModel : ViewModelBase
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
        //Edited Affiliation - Affiliation object that holds data before insertion into ActiveProfile.AffiliationsList
        private ProffessionalAffiliation editedAffiliation = new ProffessionalAffiliation();
        public ProffessionalAffiliation EditedAffiliation
        {
            get { return editedAffiliation; }
            set
            {
                editedAffiliation = value;
                OnPropertyChanged(nameof(EditedAffiliation));
            }
        }
        //Commands
        public RelayCommand<object> EditNewAffiliationCommand { get; private set; }
        public RelayCommand<object> SubmitNewAffiliationCommand { get; private set; }
        #endregion

        #region Constructors
        public AffiliationsViewModel()
        {
            EditNewAffiliationCommand = new RelayCommand<object>(EditNewAffiliation);
            SubmitNewAffiliationCommand = new RelayCommand<object>(SubmitNewAffiliation);
        }
        #endregion

        #region Private Methods
        private void EditNewAffiliation(object commandParameter)
        {
            editedAffiliation.IsSelected= true;
        }

        private void SubmitNewAffiliation(object commandParameter)
        {
            ActiveProfile.AffiliationsList.Insert(0, EditedAffiliation);
            EditedAffiliation = new ProffessionalAffiliation { IsSelected=false };
        }
        #endregion
    }
}
