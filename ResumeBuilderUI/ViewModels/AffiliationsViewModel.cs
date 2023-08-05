using ResumeBuilderUI.Models;
using ResumeBuilderUI.UserControls;
using System;

namespace ResumeBuilderUI.ViewModels
{
    class AffiliationsViewModel : ViewModelBase
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

        public RelayCommand<object> EditNewAffiliationCommand { get; private set; }
        public RelayCommand<object> SubmitNewAffiliationCommand { get; private set; }

        public AffiliationsViewModel()
        {
            EditNewAffiliationCommand = new RelayCommand<object>(EditNewAffiliation);
            SubmitNewAffiliationCommand = new RelayCommand<object>(SubmitNewAffiliation);
        }

        private void EditNewAffiliation(object commandParameter)
        {
            editedAffiliation.IsSelected= true;
        }

        private void SubmitNewAffiliation(object commandParameter)
        {
            ActiveProfile.AffiliationsList.Insert(0, EditedAffiliation);
            EditedAffiliation = new ProffessionalAffiliation { IsSelected=false };
        }
    }
}
