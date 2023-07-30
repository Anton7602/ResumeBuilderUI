using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ResumeBuilderUI.ViewModels
{
    /// <summary>
    /// ViewModel for SettingView
    /// </summary>
    internal class SettingViewModel :ViewModelBase
    {
        private List<string> _availableLanguages = App.supportedLanguages.Keys.ToList();
        public List<string> AvailableLanguages
        {
            get { return _availableLanguages; }
        }

        private string selectedLanguage = Application.Current.Resources["currentLanguage"] as string;
        public string SelectedLanguage
        {
            get { return selectedLanguage; }
            set 
            {
                selectedLanguage = value;
                App.Language = App.supportedLanguages.GetValueOrDefault(value);
                OnPropertyChanged("SelectedLanguage");
            }
        }

        public SettingViewModel()
        {

        }
    }
}
