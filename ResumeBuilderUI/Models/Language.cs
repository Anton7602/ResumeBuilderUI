namespace ResumeBuilderUI.Models
{
    /// <summary>
    /// Class that stores Languages Proficiences of an Applicant
    /// </summary>
    public class Language: ResumeElementBase
    {
        #region Fields and Properties
        private string _languageName = string.Empty;
        public string LanguageName
        {
            get { return _languageName; }
            set
            {
                _languageName= value;
                OnPropertyChanged(nameof(LanguageName));
            }
        }
        private string _proficiency = string.Empty;
        public string Proficiency
        {
            get { return _proficiency; }
            set
            {
                _proficiency= value;
                OnPropertyChanged(nameof(Proficiency));
            }
        }
        #endregion

        public Language()
        {
            IsSelected = false;
        }

    }
}
