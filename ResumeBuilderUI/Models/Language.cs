using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
