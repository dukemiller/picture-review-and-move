using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using picture_review_and_move.Annotations;

namespace picture_review_and_move
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _loadPath = "Source: ";

        private string _movePath = "Source: ";

        private string _currentImagePath;

        public MainWindowViewModel()
        {
        }

        // 

        public string CurrentImagePath
        {
            get => _currentImagePath;
            set
            {
                _currentImagePath = value;
                OnPropertyChanged();
            }
        }

        public string LoadPath
        {
            get => _loadPath;
            set
            {
                _loadPath = value;
                OnPropertyChanged();
            }
        }

        public string MovePath
        {
            get => _movePath;
            set
            {
                _movePath = value;
                OnPropertyChanged();
            }
        }

        // 


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}