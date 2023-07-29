using System.Collections.Generic;

namespace ResumeBuilderUI.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool isActive { get; set; }
        public List<ApplicantProfile> Profiles { get; set; }

        public User()
        {
            Id = 1;
            Name = "Peter";
            isActive = false;
            Profiles= new List<ApplicantProfile>();
        }
    }
}
