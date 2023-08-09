using ResumeBuilderUI.Models;
using System;

namespace ResumeBuilderUI.ViewModels
{
    class ContactsViewModel : ViewModelBase
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
        private Contact _editedContact = new Contact { IsSelected = false };
        public Contact EditedContact
        {
            get { return _editedContact; }
            set
            {
                _editedContact = value;
                OnPropertyChanged(nameof(EditedContact));
            }
        }

        public RelayCommand<object> EditNewContactCommand { get; private set; }
        public RelayCommand<object> SubmitNewContactCommand { get; private set; }

        public ContactsViewModel()
        {
            EditNewContactCommand = new RelayCommand<object>(EditNewContact);
            SubmitNewContactCommand = new RelayCommand<object>(SubmitNewContact);
        }

        private void EditNewContact(object obj)
        {
            EditedContact.IsSelected= true;
        }

        private void SubmitNewContact(object obj)
        {
            ActiveProfile.ContactsList.Insert(0, EditedContact);
            EditedContact = new Contact { IsSelected= false };  
        }
    }
}
