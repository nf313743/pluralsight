using System;
using System.ComponentModel;
using System.Timers;
using MVVMHookupDemo.Customers;

namespace MVVMHookupDemo
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _notificationMessage;

        private Timer _timer = new Timer(5000);

        public MainWindowViewModel()
        {
            _timer.Elapsed += (s, e) => NotificationMessage = "Time is " + DateTime.Now.ToLocalTime();
            _timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object CurrentViewModel { get; set; } = new CustomerListViewModel();

        public string NotificationMessage
        {
            get
            {
                return _notificationMessage;
            }

            set
            {
                if (_notificationMessage != value)
                {
                    _notificationMessage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NotificationMessage)));
                }
            }
        }
    }
}