namespace ResumeBuilderUI.Models
{
    public class Language
    {
        #region Fields and Properties
        public string LanguageName { get; set; }
        public string Proficiency { get; set; }
        public bool IsSelected { get; set; }
        #endregion

        public Language()
        {
            IsSelected = false;
        }

    }
}
