using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zza.Data
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public Guid CustomerId { get; set; }

        public decimal DeliveryCharge { get; set; }

        public string DeliveryCity { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string DeliveryState { get; set; }

        public string DeliveryStreet { get; set; }

        public string DeliveryZip { get; set; }

        [Key]
        public long Id { get; set; }

        public decimal ItemsTotal { get; set; }

        public DateTime OrderDate { get; set; }

        //public Customer Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public int OrderStatusId { get; set; }

        public string Phone { get; set; }

        public OrderStatus Status { get; set; }

        public Guid? StoreId { get; set; }
    }
}