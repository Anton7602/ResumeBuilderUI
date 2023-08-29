using ResumeBuilderUI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace ResumeBuilderUI.Views
{
    /// <summary>
    /// Main Window of ResumeBuilderUI app
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.Language = new CultureInfo(ResumeBuilderUI.Properties.Settings.Default.DefaultLanguage);
            try
            {
                using (StreamReader profileReader = new StreamReader(@"profiles\" + ResumeBuilderUI.Properties.Settings.Default.DefaultProfile))
                {
                    string profileJsonLine = profileReader.ReadToEnd();
                    App.ActiveProfile = JsonSerializer.Deserialize<ApplicantProfile>(profileJsonLine);
                }
            }
            catch (Exception ex)
            {
                App.ActiveProfile = new ApplicantProfile();
            }

            DirectoryInfo d = new DirectoryInfo(@"profiles");
            if (d.Exists)
            {
                FileInfo[] filesArray = d.GetFiles("*.cvp");
                List<string> profileNames = new List<string>();
                foreach (FileInfo file in filesArray)
                {
                    profileNames.Add(file.ToString().Substring(file.ToString().LastIndexOf(@"\") + 1));
                }
                ProfileSelectionCombobox.ItemsSource = profileNames;
            }
            else
            {
                Directory.CreateDirectory(d.ToString());
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ProfileSelectionCombobox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                using (StreamReader profileReader = new StreamReader(@"profiles\" + (sender as ComboBox).SelectedItem))
                {
                    string profileJsonLine = profileReader.ReadToEnd();
                    App.ActiveProfile = JsonSerializer.Deserialize<ApplicantProfile>(profileJsonLine);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
