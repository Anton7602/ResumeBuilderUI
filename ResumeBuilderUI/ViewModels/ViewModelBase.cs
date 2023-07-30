using System.ComponentModel;

namespace ResumeBuilderUI.ViewModels
{
    /// <summary>
    /// Base implementation of ViewModel functionality
    /// </summary>
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName = null));
        }
    }
}
