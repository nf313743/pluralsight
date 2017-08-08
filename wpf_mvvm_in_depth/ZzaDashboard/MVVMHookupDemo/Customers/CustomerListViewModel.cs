using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMHookupDemo.Services;
using Zza.Data;

namespace MVVMHookupDemo.Customers
{
    public class CustomerListViewModel
    {
        public ObservableCollection<Customer> Customers { get; set; }

        private ICustomersRepository _repo;

        public CustomerListViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                return;
            }
            _repo = new CustomersRepository();
            Customers = new ObservableCollection<Customer>(_repo.GetCustomersAsync().Result);
        }
    }
}