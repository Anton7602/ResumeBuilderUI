using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private ApplicantProfile _activeProfile = new ApplicantProfile();
        public ApplicantProfile ActiveProfile
        {
            get { return _activeProfile; }
            set
            {
                _activeProfile = value;
                OnPropertyChanged("Active Profile");
            }
        }
    }
}
