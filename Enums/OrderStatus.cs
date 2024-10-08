namespace EcommerceEjemploApi.Enums
{
    public enum OrderStatus
    {
        Pending,  //Pedido pendiente de confirm o procesamiento
        Processing, //Siendo procesado, preparacion pedidos,verificacion inventario generar etiqueta envio
        Shipped,  //El pedido ha sido enviado (en camino)
        Delivered,  //Entregado
        Cancelled  //Cancelado
    }
}
