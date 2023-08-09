using System.ComponentModel;

namespace ResumeBuilderUI.Models
{
    public class Contact :INotifyPropertyChanged
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
        //Is Selected - Defines if this particular contact will be used by ResumeBuilder. Also helps with showing/hiding UI elements
        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        #endregion

        #region Constructors
        public Contact() { }
        #endregion

        #region Interface Implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName = null));
        }
        #endregion
    }
}
