using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ResumeBuilderUI
{
    public partial class App : Application
    {
        private static List<CultureInfo> supportedLanguages= new List<CultureInfo>();
        public static List<CultureInfo> SupportedLanguages { get { return supportedLanguages; } }
        public static event EventHandler LanguageChanged;
        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if(value == null) throw new ArgumentNullException("value");
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dict = new ResourceDictionary();
                switch(value.Name)
                {
                    case "ru-RU":
                        dict.Source = new Uri("Resourses/Localization/lang" + value.Name + ".xaml", UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Resourses/lang.xaml", UriKind.Relative);
                        break;
                }
                ResourceDictionary replacedDictionary = (from d in Application.Current.Resources.MergedDictionaries
                                                         where d.Source!= null && d.Source.OriginalString.StartsWith("Resourses/Localization/lang.")
                                                         select d).First();
                if (replacedDictionary != null)
                {
                    int i = Application.Current.Resources.MergedDictionaries.IndexOf(replacedDictionary);
                    Application.Current.Resources
                }
            }
        }


        public App()
        {
            supportedLanguages.Clear();
            supportedLanguages.Add(new CultureInfo("en-US"));
            supportedLanguages.Add(new CultureInfo("ru-RU"));
        }
    }
}
