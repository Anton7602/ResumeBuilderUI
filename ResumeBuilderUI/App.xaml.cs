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

                ResourceDictionary newDictionary = new ResourceDictionary();
                switch(value.Name)
                {
                    case "ru-RU":
                        newDictionary.Source = new Uri("Resourses/Localization/lang." + value.Name + ".xaml", UriKind.Relative);
                        break;
                    default:
                        newDictionary.Source = new Uri("Resourses/Localization/lang.xaml", UriKind.Relative);
                        break;
                }
                ResourceDictionary oldDictionary = (from d in Application.Current.Resources.MergedDictionaries
                                                         where d.Source!= null && d.Source.OriginalString.StartsWith("Resourses/Localization/lang.")
                                                         select d).First();
                if (oldDictionary != null)
                {
                    int i = Application.Current.Resources.MergedDictionaries.IndexOf(oldDictionary);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDictionary);
                    Application.Current.Resources.MergedDictionaries.Insert(i, newDictionary);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(newDictionary);
                }
                LanguageChanged(Application.Current, new EventArgs());
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
