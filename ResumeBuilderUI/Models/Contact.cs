namespace ResumeBuilderUI.Models
{
    public class Contact
    {
        public string ContactType { get; set; }
        public string ContactDescription { get; set; }
        public bool IsSelected { get; set; }

        public Contact()
        {
            IsSelected = false;
        }
    }
}
