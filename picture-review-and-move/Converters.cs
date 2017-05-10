using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace picture_review_and_move
{
    public class UriToCachedImageConverter : IValueConverter
    {
        // http://www.thejoyofcode.com/WPF_Image_element_locks_my_local_file.aspx
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(value?.ToString()))
            {
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(value.ToString());
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
                return bi;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("Two way conversion is not supported.");
        }
    }
}