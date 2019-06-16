using System.Collections.Generic;
using DutchTreat.Controllers;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);

        bool SaveAll();
        Order GetOrderById(int id);
        void AddEntity(object model);
    }
}