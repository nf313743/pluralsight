using System;
using System.Collections.ObjectModel;
using Zza.Data;
using ZzaDesktop.Services;

namespace ZzaDesktop.Customers
{
    internal class CustomerListViewModel : BindableBase
    {
        private ObservableCollection<Customer> _customer;
        private ICustomersRepository _repo = new CustomersRepository();

        public CustomerListViewModel()
        {
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
        }

        public event EventHandler<Customer> AddCustomerRequested = delegate { };

        public event EventHandler<Customer> EditCustomerRequested = delegate { };

        public event EventHandler<Guid> PlaceOrderRequested;

        public RelayCommand AddCustomerCommand { get; private set; }

        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customer;
            }

            set
            {
                SetProperty(ref _customer, value);
            }
        }

        public RelayCommand<Customer> EditCustomerCommand { get; private set; }

        public RelayCommand<Customer> PlaceOrderCommand { get; private set; }

        public async void LoadCustomers()
        {
            this.Customers = new ObservableCollection<Customer>(await _repo.GetCustomersAsync());
        }

        private void OnAddCustomer()
        {
            AddCustomerRequested(this, new Customer { Id = Guid.NewGuid() });
        }

        private void OnEditCustomer(Customer cust)
        {
            EditCustomerRequested(this, cust);
        }

        private void OnPlaceOrder(Customer customer)
        {
            PlaceOrderRequested?.Invoke(this, customer.Id);
        }
    }
}