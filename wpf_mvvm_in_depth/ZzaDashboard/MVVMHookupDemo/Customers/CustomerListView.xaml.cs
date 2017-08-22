using System.Windows;
using System.Windows.Controls;
using Zza.Data;

namespace MVVMHookupDemo.Customers
{
    /// <summary>
    /// Interaction logic for CustomerListView.xaml
    /// </summary>
    public partial class CustomerListView : UserControl
    {
        public CustomerListView()
        {
            InitializeComponent();
        }

        private void OnChangeCustomer(object sender, RoutedEventArgs e)
        {
            var cust = customerDataGrid.SelectedItem as Customer;
            cust.FirstName = "Changed in background";
        }
    }
}