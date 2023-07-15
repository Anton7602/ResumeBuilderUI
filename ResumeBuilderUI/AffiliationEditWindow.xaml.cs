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
    public partial class AffiliationEditWindow : Window
    {
        public ProffessionalAffiliation editedAffiliation;
        public AffiliationEditWindow()
        {
            InitializeComponent();
            editedAffiliation= new ProffessionalAffiliation();
        }

        public AffiliationEditWindow(ProffessionalAffiliation affiliation)
        {
            InitializeComponent();
            editedAffiliation= affiliation;
            FillRecievedAffiliationDataInFields();

        }

        private void FillRecievedAffiliationDataInFields()
        {
            txtAffiliantCompany.Text = editedAffiliation.Company;
            txtAffiliation.Text = editedAffiliation.Affiliation;
            dpckrDateOfAffiliation.SelectedDate = editedAffiliation.DateOfAffiliation;
        }

        private void btnAffiliationAccept_Click(object sender, RoutedEventArgs e)
        {
            editedAffiliation.Company = txtAffiliantCompany.Text;
            editedAffiliation.Affiliation= txtAffiliation.Text;
            editedAffiliation.DateOfAffiliation = (DateTime)dpckrDateOfAffiliation.SelectedDate;
            this.DialogResult = true;
            this.Close();
        }
    }
}
