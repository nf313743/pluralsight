using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zza.Data;

namespace ZzaDashboard.Services
{
    public interface IOrdersRepository
    {
        Task<Order> AddOrderAsync(Order order);

        Task DeleteOrderAsync(int orderId);

        Task<List<Order>> GetAllOrdersAsync();

        Task<List<Order>> GetOrdersForCustomersAsync(Guid customerId);

        Task<List<OrderStatus>> GetOrderStatusesAsync();

        Task<List<ProductOption>> GetProductOptionsAsync();

        Task<List<Product>> GetProductsAsync();

        Task<List<ProductSize>> GetProductSizesAsync();

        Task<Order> UpdateOrderAsync(Order order);
    }
}