using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Zza.Data;
using ZzaDesktop.Services;

namespace ZzaDesktop.Customers
{
    internal class CustomerListViewModel : BindableBase
    {
        private List<Customer> _allCustomers;

        private ObservableCollection<Customer> _customer;

        private ICustomersRepository _repo;

        private string _searchInput;

        public CustomerListViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }

        public event EventHandler<Customer> AddCustomerRequested = delegate { };

        public event EventHandler<Customer> EditCustomerRequested = delegate { };

        public event EventHandler<Guid> PlaceOrderRequested;

        public RelayCommand AddCustomerCommand { get; private set; }

        public RelayCommand ClearSearchCommand { get; private set; }

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

        public string SearchInput
        {
            get
            {
                return _searchInput;
            }

            set
            {
                SetProperty(ref _searchInput, value);
                FilterCustomers(_searchInput);
            }
        }

        public async void LoadCustomers()
        {
            _allCustomers = await _repo.GetCustomersAsync();
            this.Customers = new ObservableCollection<Customer>(_allCustomers);
        }

        private void FilterCustomers(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                Customers = new ObservableCollection<Customer>(_allCustomers);
            }
            else
            {
                Customers = new ObservableCollection<Customer>(_allCustomers.Where(c => c.FullName.ToLowerInvariant().Contains(searchInput)));
            }
        }

        private void OnAddCustomer()
        {
            AddCustomerRequested(this, new Customer { Id = Guid.NewGuid() });
        }

        private void OnClearSearch()
        {
            SearchInput = null;
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