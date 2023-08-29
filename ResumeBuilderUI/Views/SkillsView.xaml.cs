using ResumeBuilderUI.Models;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ResumeBuilderUI.Views
{
    /// <summary>
    /// Interaction logic for SkillsView.xaml
    /// </summary>
    public partial class SkillsView : UserControl
    {
        public SkillsView()
        {
            InitializeComponent();
        }

        private void ToggleButton_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Skillset skillsetToRemove = new Skillset();
            foreach(Skillset skillset in App.ActiveProfile.SkillsetsList)
            {
                if(skillset.MainSkill.Equals((sender as ToggleButton).Content))
                {
                    skillsetToRemove = skillset;
                    break;
                }
            }
            if(!skillsetToRemove.MainSkill.Equals(string.Empty))
            {
                App.ActiveProfile.SkillsetsList.Remove(skillsetToRemove);
            }
        }
    }
}
