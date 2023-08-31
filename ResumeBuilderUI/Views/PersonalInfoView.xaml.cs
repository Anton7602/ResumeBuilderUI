using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ResumeBuilderUI.Views
{
    /// <summary>
    /// Interaction logic for PersonalInfoView.xaml
    /// </summary>
    public partial class PersonalInfoView : UserControl
    {
        public PersonalInfoView()
        {
            InitializeComponent();
            try
            {
                AvatarImageBrush.ImageSource = new BitmapImage(new Uri(App.ActiveProfile.AvatarImagePath));
            }
            catch (Exception ex)
            {
                using (MemoryStream memoryStream = new MemoryStream(Properties.Resources.AvatarDefault))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();
                    AvatarImageBrush.ImageSource = bitmapImage;
                }
            }
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
                App.ActiveProfile.AvatarImagePath = dialog.FileName;
            }
        }
    }
}
