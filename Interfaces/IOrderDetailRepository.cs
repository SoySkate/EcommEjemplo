using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Interfaces
{
    public interface IOrderDetailRepository
    {
        ICollection<OrderDetail> GetOrdersDetails();
        ICollection<OrderDetail> GetOrdersDetailsOfAnOrder(Order order);
        OrderDetail GetOrderDetail(int orderDetailId);
        //OrderDetail GetOrderDetail(Product product);
        bool OrderDetailExists(int orderDetailId);
        bool CreateOrderDetail(OrderDetail orderDetail);
        bool UpdateOrderDetail(OrderDetail orderDetail);
        bool DeleteOrderDetail(OrderDetail orderDetail);
        bool Save();


    }
}
