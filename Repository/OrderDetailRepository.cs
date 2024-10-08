using EcommerceEjemploApi.Data;
using EcommerceEjemploApi.Interfaces;
using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private DataContext _context;

        public OrderDetailRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateOrderDetail(OrderDetail orderDetail)
        {
            _context.Add(orderDetail);
            return Save();
        }

        public bool DeleteOrderDetail(OrderDetail orderDetail)
        {
            _context.Remove(orderDetail);
            return Save();
        }

        public OrderDetail GetOrderDetail(int orderDetailId)
        {
            return _context.OrdersDetails.Where(od => od.Id == orderDetailId).FirstOrDefault();
        }

        public ICollection<OrderDetail> GetOrdersDetails()
        {
            return _context.OrdersDetails.OrderBy(od=>od.Id).ToList();
        }

        public ICollection<OrderDetail> GetOrdersDetailsOfAnOrder(Order order)
        {
            return _context.OrdersDetails.Where(o=>o.Order.Id == order.Id).ToList();
        }

        public bool OrderDetailExists(int orderDetailId)
        {
            return _context.OrdersDetails.Any(od=>od.Id== orderDetailId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOrderDetail(OrderDetail orderDetail)
        {
            _context.Update(orderDetail);
            return Save();
        }
    }
}
