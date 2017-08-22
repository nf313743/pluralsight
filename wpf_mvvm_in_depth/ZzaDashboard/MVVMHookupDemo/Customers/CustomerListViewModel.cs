using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using MVVMHookupDemo.Services;
using Zza.Data;

namespace MVVMHookupDemo.Customers
{
    public class CustomerListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Customer> _customers;

        private ICustomersRepository _repo;

        private Customer _selectedCustomer;

        public CustomerListViewModel()
        {
            _repo = new CustomersRepository();

            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }

            set
            {
                if (_customers != value)
                {
                    _customers = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Customers)));
                }
            }
        }

        public RelayCommand DeleteCommand { get; private set; }

        public Customer SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }

            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    DeleteCommand.RaiseCanExecuteChanged();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCustomer)));
                }
            }
        }

        public async Task LoadCustomers()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                return;
            }
            Customers = new ObservableCollection<Customer>(await _repo.GetCustomersAsync());
        }

        private bool CanDelete()
        {
            if (_selectedCustomer != null)
            {
                return true;
            }

            return false;
        }

        private void OnDelete()
        {
            Customers.Remove(SelectedCustomer);
        }
    }
}