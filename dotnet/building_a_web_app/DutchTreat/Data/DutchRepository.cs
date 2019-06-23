using System;
using System.Collections.Generic;
using System.Linq;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _context;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {

                _logger.LogInformation("GetAllProducts called");
                return _context.Products.OrderBy(x => x.Title).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _context.Orders
                            .Include(x => x.Items)
                            .ThenInclude(x => x.Product)
                            .ToList();
            }
            else
            {
                return _context.Orders.ToList();
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products.Where(x => x.Category == category).ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public Order GetOrderById(string username, int id)
        {
            return _context.Orders
                            .Include(x => x.Items)
                            .ThenInclude(x => x.Product)
                            .Where(x => x.Id == id && x.User.UserName == username)
                            .FirstOrDefault();
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        public IEnumerable<Order> GetAllOrderByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _context.Orders
                            .Where(x => x.User.UserName == username)
                            .Include(x => x.Items)
                            .ThenInclude(x => x.Product)
                            .ToList();
            }
            else
            {
                return _context.Orders
                                .Where(x => x.User.UserName == username)
                                .ToList();
            }
        }

        public void AddOrder(Order newOrder)
        {
            foreach (var item in newOrder.Items)
            {
                item.Product = _context.Products.Find(item.Product.Id);
            }

            AddEntity(newOrder);
        }
    }
}