namespace EcommerceEjemploApi.Enums
{
    //Simples enums se pueden usar por nombre o numeros en orden
    public enum OrderStatus
    {
        Pending,  //Pedido pendiente de confirm o procesamiento
        Processing, //Siendo procesado, preparacion pedidos,verificacion inventario generar etiqueta envio
        Shipped,  //El pedido ha sido enviado (en camino)
        Delivered,  //Entregado
        Cancelled  //Cancelado
    }
}
