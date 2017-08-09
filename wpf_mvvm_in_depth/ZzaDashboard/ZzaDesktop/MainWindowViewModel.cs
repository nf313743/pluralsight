using System;
using Zza.Data;
using ZzaDesktop.Customers;
using ZzaDesktop.OrderPrep;
using ZzaDesktop.Orders;

namespace ZzaDesktop
{
    internal class MainWindowViewModel : BindableBase
    {
        private BindableBase _currentViewModel;
        private CustomerListViewModel _customerListViewModel = new CustomerListViewModel();
        private OrderPrepViewModel _orderPrepViewModel = new OrderPrepViewModel();
        private OrderViewModel _orderViewModel = new OrderViewModel();
        private AddEditCustomerViewModel _addEditViewModel = new AddEditCustomerViewModel();

        public MainWindowViewModel()
        {
            NavCommand = new RelayCommand<string>(OnNav);
            _customerListViewModel.PlaceOrderRequested += NavToOrder;
            _customerListViewModel.AddCustomerRequested += NavToAddCustomer;
            _customerListViewModel.EditCustomerRequested += NavToEditCustomer;
        }

        private void NavToEditCustomer(object sender, Customer cust)
        {
            _addEditViewModel.EditMode = false;
            _addEditViewModel.SetCustomer(cust);
            CurrentViewModel = _addEditViewModel;
        }

        private void NavToAddCustomer(object sender, Customer cust)
        {
            _addEditViewModel.EditMode = true;
            _addEditViewModel.SetCustomer(cust);
            CurrentViewModel = _addEditViewModel;
        }

        public BindableBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }

            set
            {
                SetProperty(ref _currentViewModel, value);
            }
        }

        public RelayCommand<string> NavCommand { get; private set; }

        private void NavToOrder(object sender, Guid customerId)
        {
            _orderViewModel.CustomerId = customerId;
            CurrentViewModel = _orderViewModel;
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "orderPrep":
                    CurrentViewModel = _orderPrepViewModel;
                    break;

                case "customers":
                default:
                    CurrentViewModel = _customerListViewModel;
                    break;
            }
        }
    }
}