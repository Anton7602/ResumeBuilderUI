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
    public partial class SkillEditWindow : Window
    {
        public string editedSkill;
        public SkillEditWindow()
        {
            InitializeComponent();
        }

        public SkillEditWindow(string skill)
        {
            InitializeComponent();
            editedSkill = skill;
            txtSkillName.Text = editedSkill;
        }

        private void btnSkillNameAccept_Click(object sender, RoutedEventArgs e)
        {
            editedSkill = txtSkillName.Text;
            this.DialogResult = true;
            this.Close();
        }
    }
}
