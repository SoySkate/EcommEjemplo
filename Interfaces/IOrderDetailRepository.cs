using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Interfaces
{
    //funciones que hara el repository
    //La interface se comunica directamente con el repository (bueno lo implementa)
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
