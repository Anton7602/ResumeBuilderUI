using ResumeBuilderUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ResumeBuilderUI
{
    public partial class App : Application
    {
        public static ApplicantProfile ActiveProfile { get; set; } 

        public static readonly Dictionary<string, CultureInfo> supportedLanguages = new Dictionary<string, CultureInfo>()
        {
            { "English - English", new CultureInfo("en-US") },
            { "Russian - Русский", new CultureInfo("ru-RU") }
        };
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

        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            ResumeBuilderUI.Properties.Settings.Default.DefaultLanguage = Language.Name;
            ResumeBuilderUI.Properties.Settings.Default.Save();
        }


        public App()
        {
            InitializeComponent();
            ActiveProfile=new ApplicantProfile();
            App.LanguageChanged += App_LanguageChanged;
        }
    }
}
