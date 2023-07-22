using ResumeBuilderUI.ViewModels;
using ResumeBuilderUI.Views;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace ResumeBuilderUI.UserControls
{
    public partial class TextField : UserControl, INotifyPropertyChanged
    {   
        public TextField()
        {
            InitializeComponent();
        }

        private bool isTextEmpty = true;
        public bool IsTextEmpty
        {
            get { return isTextEmpty; }
            set
            {
                isTextEmpty = value;
                OnPropertyChanged("IsTextEmpty");
            }
        }

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(TextField));

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(TextField));


        public string Text
        {
            get 
            { 
                return (string)GetValue(TextProperty);
            }
            set 
            {
                SetValue(TextProperty, value); 
            }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextField), 
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTextChanged)));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TextField).TextFieldText = e.NewValue.ToString();
            if ((d as TextField).TextFieldText != null)
            {
                (d as TextField).IsTextEmpty = (d as TextField).TextFieldText.Equals(string.Empty);
            }
        }

        private string textFieldText;
        public string TextFieldText
        {
            get { return textFieldText; }
            set
            {
                textFieldText = value;
                isTextEmpty = value.Equals(string.Empty);
                OnPropertyChanged("TextFieldText");
                SetValue(TextProperty, value);
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName = null));
        }
    }
}
