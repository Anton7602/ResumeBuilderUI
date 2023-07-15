using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ResumeBuilderUI
{
    public partial class ExperienceTagEditWindow : Window
    {
        public string editedExperienceTag;
        public ExperienceTagEditWindow()
        {
            InitializeComponent();
        }

        public ExperienceTagEditWindow(string tag)
        {
            InitializeComponent();
            this.editedExperienceTag = tag;
            txtExperienceTag.Text = editedExperienceTag;
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            editedExperienceTag = txtExperienceTag.Text;
            this.DialogResult = true;
            this.Close();
        }
    }
}
