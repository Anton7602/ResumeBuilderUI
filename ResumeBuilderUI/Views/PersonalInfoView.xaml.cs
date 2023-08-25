using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ResumeBuilderUI.Views
{
    /// <summary>
    /// Interaction logic for PersonalInfoView.xaml
    /// </summary>
    public partial class PersonalInfoView : UserControl
    {
        public string avatarImagePath;
        public PersonalInfoView()
        {
            InitializeComponent();
        }

        private void Ellipse_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Avatar";
            dialog.DefaultExt = ".png"; // Default file extension
            dialog.Filter = "Image files (.png)|*.png"; // Filter files by extension
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                AvatarImageBrush.ImageSource = new BitmapImage(new System.Uri(dialog.FileName));
                avatarImagePath= dialog.FileName;
            }
        }
    }
}
