using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderUI.Models
{
    /// <summary>
    /// Base Implementation of INotifyPropertyChanged to inheret by classes involved in UI
    /// </summary>
    public class ResumeElementBase : INotifyPropertyChanged
    {
        #region Fields and Properties
        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
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
