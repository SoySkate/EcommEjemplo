using EcommerceEjemploApi.Enums;
using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();
        ICollection<Order> GetOrdersByUser(int userId);
        Order GetOrder(int orderId);
        Order GetOrder(decimal orderPrice);

        //this is getting by datetime:
        Order GetOrder(DateTime dateOrder);

        //this is getting by enum:
        Order GetOrder(OrderStatus orderStatus);
        Order GetOrderByOrderDetail(int orderDetailId);
        bool OrderExists(int orderId);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order orderId);
        bool Save();
        

    }
}
