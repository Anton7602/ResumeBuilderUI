using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ResumeBuilderUI.UserControls
{
    /// <summary>
    /// Interaction logic for TextFieldButton.xaml
    /// </summary>
    public partial class TextFieldButton : UserControl, INotifyPropertyChanged
    {
        #region Fields and Properties
        private string _textFieldButtonText;
        public string TextFieldButtonText
        {
            get { return _textFieldButtonText; }
            set
            {
                _textFieldButtonText = value;
                OnPropertyChanged(nameof(TextFieldButtonText));
                SetValue(TextProperty, value);
            }
        }
        #endregion

        #region Constructors
        public TextFieldButton()
        {
            InitializeComponent();
        }
        #endregion

        #region UserControl Properties
        //AcceptCommand - Property to bind a ViewModel command, that triggers upon pressing accept button of a TextFieldButton
        public static readonly DependencyProperty AcceptCommandProperty =
            DependencyProperty.Register("AcceptCommand", typeof(ICommand), typeof(TextFieldButton));
        public ICommand AcceptCommand
        {
            get { return (ICommand)GetValue(AcceptCommandProperty); }
            set { SetValue(AcceptCommandProperty, value); }
        }
        //Text - Property that binds text to a TextBox within TextFieldButton
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextFieldButton),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTextChanged)));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TextFieldButton).TextFieldButtonText = e.NewValue.ToString();
        }
        #endregion

        #region Private Methods
        private void ShowTextButton_Click(object sender, RoutedEventArgs e)
        {
            TextFieldButtonIcon.Visibility = Visibility.Collapsed;
            TextFieldButtonTextBox.Visibility = Visibility.Visible;
            TextFieldSubmitButton.Visibility = Visibility.Visible;
            TextFieldCancelButton.Visibility = Visibility.Visible;
            TextFieldButtonTextBox.Focus();
        }

        private void SubmitInput()
        {
            if (TextFieldButtonTextBox.Text != string.Empty)
            {
                TextFieldSubmitButton.Command.Execute(
                    TextFieldButtonTextBox.Text.Substring(0,1).ToUpper() + TextFieldButtonTextBox.Text.Substring(1));
            }
        }

        private void HideTextButton()
        {
            TextFieldButtonIcon.Visibility = Visibility.Visible;
            TextFieldButtonTextBox.Visibility = Visibility.Collapsed;
            TextFieldSubmitButton.Visibility = Visibility.Collapsed;
            TextFieldCancelButton.Visibility = Visibility.Collapsed;
            TextFieldButtonTextBox.Text = string.Empty;
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
                if(TextFieldButtonTextBox.IsFocused)
                {
                    SubmitInput();
                    HideTextButton();
                }
            }
            if(e.Key==Key.Escape)
            {
                if(TextFieldButtonTextBox.IsFocused)
                {
                    HideTextButton();
                }
            }
        }

        private void TextFieldCancelButton_Click(object sender, RoutedEventArgs e)
        {
            HideTextButton();
        }
        #endregion

        #region Interface Implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName = null));
        }
        #endregion
    }
}
