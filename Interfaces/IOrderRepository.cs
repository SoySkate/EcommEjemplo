using EcommerceEjemploApi.Enums;
using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Interfaces
{
    //funciones que hara el repository
    //La interface se comunica directamente con el repository (bueno lo implementa)
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
