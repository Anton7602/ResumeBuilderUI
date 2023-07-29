namespace ResumeBuilderUI.Models
{
    public class Language
    {
        public string LanguageName { get; set; }
        public string Proficiency { get; set; }
        public bool IsSelected { get; set; }

        public Language()
        {
            IsSelected = false;
        }

    }
}
