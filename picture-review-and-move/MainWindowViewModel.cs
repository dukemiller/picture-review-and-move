using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using picture_review_and_move.Annotations;
using picture_review_and_move.Properties;

namespace picture_review_and_move
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _loadPath = "Source: ";

        private string _movePath = "Source: ";

        private ObservableCollection<string> _images = new ObservableCollection<string>();

        private string _currentImagePath;

        private int _currentIndex = 0;

        private static readonly string[] Extensions = {"jpg", "jpeg", "png", "gif"};

        public MainWindowViewModel()
        {
            NextCommand = new RelayCommand(Next);
            PreviousCommand = new RelayCommand(Previous);
            MoveImageCommand = new RelayCommand(MoveImage);
            LoadPath = Settings.Default.LoadPath;
            MovePath = Settings.Default.MovePath;
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

        public ObservableCollection<string> Images
        {
            get => _images;
            set
            {
                _images = value;
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
                if (Directory.Exists(LoadPath))
                {
                    if (!Settings.Default.LoadPath.Equals(LoadPath))
                    {
                        Settings.Default.LoadPath = LoadPath;
                        Settings.Default.Save();
                    }
                    
                    Images = new ObservableCollection<string>(Directory
                        .GetFiles(LoadPath)
                        .Where(file => Extensions.Any(ext => file.ToLower().EndsWith(ext)))
                    );
                    _currentIndex = 0;
                    if (Images.Count > 0)
                        CurrentImagePath = Images[_currentIndex];
                }
            }
        }

        public string MovePath
        {
            get => _movePath;
            set
            {
                _movePath = value;
                OnPropertyChanged();
                if (Directory.Exists(MovePath) && !Settings.Default.MovePath.Equals(MovePath))
                {
                    Settings.Default.MovePath = MovePath;
                    Settings.Default.Save();
                }
            }
        }

        // 

        public RelayCommand NextCommand { get; set; }

        public RelayCommand PreviousCommand { get; set; }

        public RelayCommand MoveImageCommand { get; set; }

        // 

        private void Next()
        {
            if (Images.Count > 1)
            {
                _currentIndex = ++_currentIndex % Images.Count;
                CurrentImagePath = Images[_currentIndex];
            }
        }

        private void Previous()
        {
            if (Images.Count > 1)
            {
                _currentIndex = --_currentIndex < 0 ? Images.Count - 1 : _currentIndex;
                CurrentImagePath = Images[_currentIndex];
            }
        }

        private void MoveImage()
        {
            if (Directory.Exists(MovePath) && CurrentImagePath != null && File.Exists(CurrentImagePath))
            {
                var previousPath = CurrentImagePath;
                var newPath = Path.Combine(MovePath, Path.GetFileName(CurrentImagePath));
                Images.Remove(CurrentImagePath);
                if (_currentIndex >= Images.Count)
                    _currentIndex = Images.Count - 1;
                CurrentImagePath = Images[_currentIndex];
                File.Move(previousPath, newPath);
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