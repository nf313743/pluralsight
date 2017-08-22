using System;
using System.ComponentModel;
using Zza.Data;
using ZzaDesktop.Services;

namespace ZzaDesktop.Customers
{
    internal class AddEditCustomerViewModel : BindableBase
    {
        public EventHandler Done;

        private SimpleEditableCustomer _customer;

        private Customer _editingCustomer;

        private bool _editMode;

        private ICustomersRepository _repo;

        public AddEditCustomerViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }

        public RelayCommand CancelCommand { get; private set; }

        public SimpleEditableCustomer Customer
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

        public bool EditMode
        {
            get
            {
                return _editMode;
            }

            set
            {
                SetProperty(ref _editMode, value);
            }
        }

        public RelayCommand SaveCommand { get; private set; }

        public void SetCustomer(Customer cust)
        {
            _editingCustomer = cust;
            if (Customer != null)
            {
                Customer.ErrorsChanged -= RaiseCanExecuteChanged;
            }
            Customer = new SimpleEditableCustomer();
            Customer.ErrorsChanged += RaiseCanExecuteChanged;
            CopyCustomer(cust, Customer);
        }

        private bool CanSave()
        {
            return !Customer.HasErrors;
        }

        private void CopyCustomer(Customer source, SimpleEditableCustomer target)
        {
            target.Id = source.Id;
            if (EditMode)
            {
                target.FirstName = source.FirstName;
                target.LastName = source.LastName;
                target.Phone = source.Phone;
                target.Email = source.Email;
            }
        }

        private void OnCancel()
        {
            Done?.Invoke(this, null);
        }

        private async void OnSave()
        {
            UpdateCustomer(Customer, _editingCustomer);
            if (EditMode)
            {
                await _repo.UpdateCustomerAsync(_editingCustomer);
            }
            else
            {
                await _repo.AddCustomerAsync(_editingCustomer);
            }

            Done?.Invoke(this, null);
        }

        private void RaiseCanExecuteChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void UpdateCustomer(SimpleEditableCustomer source, Customer target)
        {
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.Phone = source.Phone;
            target.Email = source.Email;
        }
    }
}