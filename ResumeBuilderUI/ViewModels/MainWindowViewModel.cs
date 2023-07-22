using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ResumeBuilderUI.Models;


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

        private Visibility _settingsVisibility = Visibility.Collapsed;
        public Visibility SettingsVisibility
        {
            get { return _settingsVisibility; }
            set
            {
                _settingsVisibility = value;
                OnPropertyChanged("SettingVisibility");
            }
        }
        private Visibility _navBarVisibility = Visibility.Visible;
        public Visibility NavBarVisibility
        {
            get { return _navBarVisibility; }
            set
            {
                _navBarVisibility = value;
                OnPropertyChanged("NavBarVisibility");
            }
        }

        private RelayCommand settingControlCommand;
        public ICommand SettingControlCommand
        {
            get
            {
                if (settingControlCommand == null)
                {
                    settingControlCommand = new RelayCommand(param => this.ControlSetting(), param => this.CanControlSetting());
                }
                return settingControlCommand;
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

        private bool CanControlSetting()
        {
            return true;
        }

        private void ControlSetting()
        {
            if(SettingsVisibility== Visibility.Visible)
            {
                SettingsVisibility = Visibility.Collapsed;
                NavBarVisibility= Visibility.Visible;
            }
            else
            {
                SettingsVisibility = Visibility.Visible;
                NavBarVisibility= Visibility.Collapsed;
            }
        }
    }
}
