using System.ComponentModel;

namespace ResumeBuilderUI.Models
{
    /// <summary>
    /// Class that stores contact information of an Applicant
    /// </summary>
    public class Contact : ResumeElementBase
    {
        #region Fields and Properties
        //Contact Type - Defines method of contact: Email, Phone, Github etc.
        private string _contactType = string.Empty;
        public string ContactType
        {
            get { return _contactType; }
            set
            {
                _contactType = value;
                OnPropertyChanged(nameof(ContactType));
            }
        }
        //Contact Description - Defines body of contact: Link to a website, phone number etc.
        private string _contactDescription = string.Empty;
        public string ContactDescription
        {
            get { return _contactDescription; }
            set
            {
                _contactDescription = value;
                OnPropertyChanged(nameof(ContactDescription));
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Base contact constructor
        /// </summary>
        public Contact() { }
        #endregion
    }
}
