using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ResumeBuilderUI.UserControls
{
    /// <summary>
    /// Interaction logic for TextFieldButton.xaml
    /// </summary>
    public partial class TextFieldButton : UserControl
    {
        public TextFieldButton()
        {
            InitializeComponent();
        }

        private void ShowTextButton_Click(object sender, RoutedEventArgs e)
        {
            TextFieldButtonIcon.Visibility = Visibility.Collapsed;
            TextFieldButtonText.Visibility = Visibility.Visible;
            TextFieldSubmitButton.Visibility = Visibility.Visible;
            TextFieldCancelButton.Visibility = Visibility.Visible;
            TextFieldButtonText.Focus();
        }

        private void SubmitInput()
        {
            if (TextFieldButtonText.Text != string.Empty)
            {
                TextFieldSubmitButton.Command.Execute(
                    TextFieldButtonText.Text.Substring(0,1).ToUpper() + TextFieldButtonText.Text.Substring(1));
            }
        }

        private void HideTextButton()
        {
            TextFieldButtonIcon.Visibility = Visibility.Visible;
            TextFieldButtonText.Visibility = Visibility.Collapsed;
            TextFieldSubmitButton.Visibility = Visibility.Collapsed;
            TextFieldCancelButton.Visibility = Visibility.Collapsed;
            TextFieldButtonText.Text = string.Empty;
        }

        private void TextFieldButtonText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextFieldSubmitButton.IsMouseOver)
            {
                SubmitInput();
            }
            HideTextButton();
        }

        private void SubmitTextFieldButton_Click(object sender, RoutedEventArgs e)
        {
            SubmitInput();
            HideTextButton();
        }

        private void TextFieldButtonText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                if(TextFieldButtonText.IsFocused)
                {
                    SubmitInput();
                    HideTextButton();
                }
            }
            if(e.Key==Key.Escape)
            {
                if(TextFieldButtonText.IsFocused)
                {
                    HideTextButton();
                }
            }
        }

        private void TextFieldCancelButton_Click(object sender, RoutedEventArgs e)
        {
            HideTextButton();
        }

        public static readonly DependencyProperty AcceptCommandProperty =
    DependencyProperty.Register("AcceptCommand", typeof(ICommand), typeof(TextFieldButton));

        public ICommand AcceptCommand
        {
            get { return (ICommand)GetValue(AcceptCommandProperty); }
            set { SetValue(AcceptCommandProperty, value); }
        }
    }
}
