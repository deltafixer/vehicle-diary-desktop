using System.Windows;
using System.Windows.Controls;

namespace VehicleDiary.Authenticate.Views
{
    public static class PasswordBoxAttach
    {
        public static readonly DependencyProperty BoundPassword = DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxAttach), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));
        private static bool _isUpdating;
        private static void OnBoundPasswordChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (_isUpdating || !(dependencyObject is PasswordBox passwordBox)) return;

            passwordBox.PasswordChanged -= OnPasswordChanged;
            passwordBox.Password = (string)dependencyPropertyChangedEventArgs.NewValue;
            passwordBox.PasswordChanged += OnPasswordChanged;
        }
        private static void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!(sender is PasswordBox passwordBox)) return;

            _isUpdating = true;
            SetBoundPassword(passwordBox, passwordBox.Password);
            _isUpdating = false;
        }
        public static string GetBoundPassword(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(BoundPassword);
        }
        public static void SetBoundPassword(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(BoundPassword, value);
        }
    }
}
