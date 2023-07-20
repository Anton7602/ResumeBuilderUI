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

        public MainWindowViewModel()
        {
            ActiveProfile = new ApplicantProfile("Binding Test");
            ActiveProfile.contactsList.Add("Email", "TestEmail@gmail.com");
            ActiveProfile.contactsList.Add("Phone", "999888777666");
            Employment employment = new Employment()
            {
                Employer = "Class Engineering",
                EndDate= DateTime.Now,
                StartDate= DateTime.Now,
                Title="Mechanical Design Engineer"
            };
            Employment employment2 = new Employment(employment);
            employment2.Employer = "Google";
            ActiveProfile.employmentsList.Add(employment);
            ActiveProfile.employmentsList.Add(employment2);
        }
    }
}
