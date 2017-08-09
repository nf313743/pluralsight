using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Timers;
using MVVMHookupDemo.Customers;

namespace MVVMHookupDemo
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Timer _timer = new Timer(5000);

        public MainWindowViewModel()
        {
            _timer.Elapsed += (s, e) => NotificationMessage = "Time is " + DateTime.Now.ToLocalTime();
            _timer.Start();
        }

        public object CurrentViewModel { get; set; } = new CustomerListViewModel();

        public event PropertyChangedEventHandler PropertyChanged;

        private string _notificationMessage;

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