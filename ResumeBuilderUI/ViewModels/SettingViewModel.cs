using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ResumeBuilderUI.ViewModels
{
    internal class SettingViewModel :ViewModelBase
    {
        private ObservableCollection<string> _availableLanguages = new ObservableCollection<string>();
        public ObservableCollection<string> AvailableLanguages
        {
            get { return _availableLanguages; }
            set 
            { 
                _availableLanguages = value;
                OnPropertyChanged("AvailableLanguages");
            }
        }

        private string selectedLanguage = string.Empty;
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
            UpdateSupportedLanguages();
            SelectedLanguage = Application.Current.Resources["currentLanguage"] as string;
        }

        private void UpdateSupportedLanguages()
        {
            string[] tempLanguages = Application.Current.Resources["lang_LanguageMenuItems"] as string[];
            CultureInfo[] tempCultures = { new CultureInfo("en-US"), new CultureInfo("ru-RU") };
            App.supportedLanguages.Clear();
            AvailableLanguages.Clear();
            for (int i = 0; i < tempLanguages.Length; i++)
            {
                App.supportedLanguages.Add(tempLanguages[i], tempCultures[i]);
                AvailableLanguages.Add(tempLanguages[i]);
            }
        }
    }
}
