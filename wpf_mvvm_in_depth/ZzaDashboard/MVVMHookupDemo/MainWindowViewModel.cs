using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMHookupDemo.Customers;

namespace MVVMHookupDemo
{
    public class MainWindowViewModel
    {
        public object CurrentViewModel { get; set; } = new CustomerListViewModel();
    }
}