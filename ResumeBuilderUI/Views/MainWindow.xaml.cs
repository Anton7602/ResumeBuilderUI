using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Printing;
using System.Resources;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResumeBuilderUI
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            App.LanguageChanged += LanguageChanged;
        }

        private void LanguageChanged(object? sender, EventArgs e)
        {
            foreach(MenuItem availableLanguage in menuLanguage.Items)
            {
                availableLanguage.IsChecked = false;
                if(App.Language.Equals(new CultureInfo(availableLanguage.Tag.ToString())))
                {
                    availableLanguage.IsChecked = true;
                }
            }
        }

        private void changeLanguageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(((sender as MenuItem)!= null) && ((sender as MenuItem).Tag != null))
            {
                App.Language = new CultureInfo((sender as MenuItem).Tag.ToString());
            }
        }
    }
}
