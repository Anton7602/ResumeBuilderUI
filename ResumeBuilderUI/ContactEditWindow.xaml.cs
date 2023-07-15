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
    public partial class ContactEditWindow : Window
    {
        public (string, string) editedContact;
        public ContactEditWindow()
        {
            InitializeComponent();
        }

        public ContactEditWindow((string, string) contact)
        {
            InitializeComponent();
            editedContact= contact;
            FillRecievedContactDataInFields();
        }

        private void FillRecievedContactDataInFields()
        {
            txtTypeOfContact.Text = editedContact.Item1;
            txtContactInfo.Text = editedContact.Item2;
        }
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            editedContact.Item1= txtTypeOfContact.Text;
            editedContact.Item2= txtContactInfo.Text;
            this.DialogResult = true;
            this.Close();
        }
    }
}
