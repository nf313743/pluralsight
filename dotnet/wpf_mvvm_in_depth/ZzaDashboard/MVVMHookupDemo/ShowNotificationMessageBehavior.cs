using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace MVVMHookupDemo
{
    public class ShowNotificationMessageBehavior : Behavior<ContentControl>
    {
        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ShowNotificationMessageBehavior), new PropertyMetadata(null, OnMessagedChanged));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += (s, e) => AssociatedObject.Visibility = Visibility.Collapsed;
        }

        private static void OnMessagedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = ((ShowNotificationMessageBehavior)d);
            behavior.AssociatedObject.Content = e.NewValue;
            behavior.AssociatedObject.Visibility = Visibility.Visible;
        }
    }
}