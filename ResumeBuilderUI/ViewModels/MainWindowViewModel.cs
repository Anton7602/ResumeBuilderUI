using ResumeBuilderUI.Models;


namespace ResumeBuilderUI.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private int LastSelectedNavBarIndex = 0;
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
        private bool _isSettingsVisible = false;
        public bool IsSettingsVisible
        {
            get { return _isSettingsVisible; }
            set 
            { 
                if(value)
                {
                    IsUserVisible = false;
                    LastSelectedNavBarIndex = NavBarSelectionIndex;
                    NavBarSelectionIndex = -1;
                }
                if(!value && NavBarSelectionIndex==-1)
                {
                    NavBarSelectionIndex = LastSelectedNavBarIndex;
                }
                _isSettingsVisible = value;
                OnPropertyChanged("IsSettingsVisible");
            }
        }
        private bool _isUserVisible = false;
        public bool IsUserVisible
        {
            get { return _isUserVisible; }
            set
            {
                if (value)
                {
                    IsSettingsVisible = false;
                    LastSelectedNavBarIndex = NavBarSelectionIndex;
                    NavBarSelectionIndex = -1;
                }
                if (!value && NavBarSelectionIndex == -1)
                {
                    NavBarSelectionIndex = LastSelectedNavBarIndex;
                }
                _isUserVisible = value;
                OnPropertyChanged("IsUserVisible");
            }
        }

        private int _navBarSelectionIndex = 0;
        public int NavBarSelectionIndex
        {
            get { return _navBarSelectionIndex; }
            set
            {
                _navBarSelectionIndex= value;
                if (value>=0)
                {
                    IsSettingsVisible = false;
                    IsUserVisible = false;
                }
                OnPropertyChanged("NavBarSelectionIndex");
            }
        }


        public MainWindowViewModel()
        {
            //db.Database.EnsureCreated();
            //db.Users.Add(new User { Name = "Anton" });
            //db.SaveChanges();
        }
    }
}
