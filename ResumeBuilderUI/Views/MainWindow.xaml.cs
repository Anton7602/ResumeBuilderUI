using ResumeBuilderUI.Models;
using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows;
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
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
