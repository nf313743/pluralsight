using System.Windows;

namespace MVVMHookupDemo
{
    public static class MvvmBehaviours
    {
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadMethodNameProperty =
            DependencyProperty.RegisterAttached("LoadedMethodName", typeof(string), typeof(MvvmBehaviours), new PropertyMetadata(null, OnLoadedMethodNameChanged));

        public static string GetLoadedMethodName(DependencyObject obj)
        {
            return (string)obj.GetValue(LoadMethodNameProperty);
        }

        public static void SetLoadedMethodName(DependencyObject obj, string value)
        {
            obj.SetValue(LoadMethodNameProperty, value);
        }

        private static void OnLoadedMethodNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = d as FrameworkElement;
            if (element != null)
            {
                element.Loaded += (s, e2) =>
                 {
                     var viewModel = element.DataContext;
                     if (viewModel == null)
                     {
                         return;
                     }
                     var methodInfo = viewModel.GetType().GetMethod(e.NewValue.ToString());
                     if (methodInfo != null)
                     {
                         methodInfo.Invoke(viewModel, null);
                     }
                 };
            }
        }
    }
}