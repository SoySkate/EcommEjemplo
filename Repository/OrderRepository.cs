using EcommerceEjemploApi.Data;
using EcommerceEjemploApi.Enums;
using EcommerceEjemploApi.Interfaces;
using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext conetxt)
        {
            _context = conetxt;
        }

        public bool CreateOrder(Order order)
        {
            _context.Add(order);
            return Save();
        }

        public bool DeleteOrder(Order orderId)
        {
            _context.Remove(orderId);
            return Save();
        }

        public Order GetOrder(int orderId)
        {
            return _context.Orders.Where(o => o.Id == orderId).FirstOrDefault();
        }

        public Order GetOrder(decimal orderPrice)
        {
            return _context.Orders.Where(o => o.TotalPrice == orderPrice).FirstOrDefault();
        }

        public Order GetOrder(DateTime dateOrder)
        {
            return _context.Orders.Where(o => o.DateTime == dateOrder).FirstOrDefault();
        }

        public Order GetOrder(OrderStatus orderStatus)
        {
            return _context.Orders.Where(o => o.OrderStatus == orderStatus).FirstOrDefault();
        }

        public Order GetOrderByOrderDetail(int orderDetailId)
        {
            var orderdetail =  _context.OrdersDetails.Where(od => od.Id == orderDetailId).FirstOrDefault();
            return orderdetail.Order;
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders.OrderBy(o=>o.Id).ToList();
        }

        public ICollection<Order> GetOrdersByUser(int userId)
        {
            return _context.Orders.Where(u=>u.User.Id==userId).ToList();
        }

        public bool OrderExists(int orderId)
        {
            return _context.Orders.Any(o=>o.Id == orderId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOrder(Order order)
        {
            _context.Update(order);
            return Save();
        }
    }
}
