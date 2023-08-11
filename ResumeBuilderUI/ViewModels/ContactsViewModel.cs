using ResumeBuilderUI.Models;
using System;

namespace ResumeBuilderUI.ViewModels
{
    class ContactsViewModel : ViewModelBase
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
        //Edited Contact - Contact object that holds data before insertion into ActiveProfile.ContactList
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
        //Commands
        public RelayCommand<object> EditNewContactCommand { get; private set; }
        public RelayCommand<object> SubmitNewContactCommand { get; private set; }
        #endregion

        #region Constructors
        public ContactsViewModel()
        {
            EditNewContactCommand = new RelayCommand<object>(EditNewContact);
            SubmitNewContactCommand = new RelayCommand<object>(SubmitNewContact);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Makes visible a contactHolder in editMode to fill in parameters of new contact
        /// </summary>
        private void EditNewContact(object obj)
        {
            EditedContact.IsSelected= true;
        }
        /// <summary>
        /// Adds contact object, specified in editedContact into ActiveProfile and resets editedContact parameters
        /// </summary>
        private void SubmitNewContact(object obj)
        {
            ActiveProfile.ContactsList.Insert(0, EditedContact);
            EditedContact = new Contact { IsSelected= false };  
        }
        #endregion
    }
}
