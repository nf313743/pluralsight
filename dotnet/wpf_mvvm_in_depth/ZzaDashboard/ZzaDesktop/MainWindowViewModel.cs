using System;
using Microsoft.Practices.Unity;
using Zza.Data;
using ZzaDesktop.Customers;
using ZzaDesktop.OrderPrep;
using ZzaDesktop.Orders;

namespace ZzaDesktop
{
    internal class MainWindowViewModel : BindableBase
    {
        private AddEditCustomerViewModel _addEditViewModel;

        private BindableBase _currentViewModel;

        private CustomerListViewModel _customerListViewModel;

        private OrderPrepViewModel _orderPrepViewModel = new OrderPrepViewModel();

        private OrderViewModel _orderViewModel = new OrderViewModel();

        public MainWindowViewModel()
        {
            NavCommand = new RelayCommand<string>(OnNav);
            _customerListViewModel = ContainerHelper.Container.Resolve<CustomerListViewModel>();
            _addEditViewModel = ContainerHelper.Container.Resolve<AddEditCustomerViewModel>();
            _customerListViewModel.PlaceOrderRequested += NavToOrder;
            _customerListViewModel.AddCustomerRequested += NavToAddCustomer;
            _customerListViewModel.EditCustomerRequested += NavToEditCustomer;
            _addEditViewModel.Done += NavToCustomerList;
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

        private void NavToAddCustomer(object sender, Customer cust)
        {
            _addEditViewModel.EditMode = false;
            _addEditViewModel.SetCustomer(cust);
            CurrentViewModel = _addEditViewModel;
        }

        private void NavToCustomerList(object sender, EventArgs e)
        {
            CurrentViewModel = _customerListViewModel;
        }

        private void NavToEditCustomer(object sender, Customer cust)
        {
            _addEditViewModel.EditMode = true;
            _addEditViewModel.SetCustomer(cust);
            CurrentViewModel = _addEditViewModel;
        }

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