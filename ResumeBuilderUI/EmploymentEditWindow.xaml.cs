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
    public partial class EmploymentEditWindow : Window
    {
        public EmploymentEditWindow()
        {
            InitializeComponent();
        }

        public EmploymentEditWindow(Employment employment)
        {
            InitializeComponent();
            Employment editedEmployment = employment;
            txtEmployerEdit.Text = employment.Employer;
            txtTitleEdit.Text = employment.Title;
            dateFrom.SelectedDate = employment.StartDate;
            dateTo.SelectedDate = employment.EndDate;
            foreach ((string, string) experience in employment.Experience)
            {
                TextBlock experienceTag = new TextBlock();
                experienceTag.Text = experience.Item1;
                TextBox experienceText = new TextBox();
                experienceText.Text = experience.Item2;
                stckpnlEmploymentEdit.Children.Add(experienceTag);
                stckpnlEmploymentEdit.Children.Add(experienceText);
                experienceText.Margin = new Thickness(5);
            }
            Button btnAccept = new Button();
            btnAccept.Content = "Accept Changes";
            btnAccept.Margin= new Thickness(10);
            btnAccept.Click += AcceptChngesButton_Click;
            stckpnlEmploymentEdit.Children.Add(btnAccept);
        }

        private void AcceptChngesButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
