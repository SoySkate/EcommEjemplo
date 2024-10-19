using AutoMapper;
using EcommerceEjemploApi.Dto;
using EcommerceEjemploApi.Interfaces;
using EcommerceEjemploApi.Models;
using EcommerceEjemploApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceEjemploApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Aqui el CONTROLLER es la API que se llama a las funciones del Sevice
    //Por ejemplo al crear un user hace el post y llama a la funcion CreateUser from Service
    //((Aqui me he saltado el Service y lo he hecho directo, el controller se conecta directo a la API
    //Cuando la GOODPRACTICE es: que el controller se conecte al service y el service al repositorio))
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderDetailController(IOrderDetailRepository orderDetailRepository,
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        //__________________________READ ALL ORDERSDETAILS
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDetail>))]
        public IActionResult GetOrdersDetails()
        {
            var ordersDetails = _mapper.Map<List<OrderDetailDto>>(_orderDetailRepository.GetOrdersDetails());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(ordersDetails);
        }

        //__________________________READ A ORDERDETAIL
        [HttpGet("{orderDetailId}")]
        [ProducesResponseType(200, Type = typeof(OrderDetail))]
        [ProducesResponseType(400)]
        public IActionResult GetOrderDetail(int orderDetailId)
        {
            if (!_orderDetailRepository.OrderDetailExists(orderDetailId)) { return NotFound(); }
            var orderDetail = _mapper.Map<OrderDetailDto>(_orderDetailRepository.GetOrderDetail(orderDetailId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderDetail);
        }

        //__________________________CREATE A ORDERDETAIL
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrderDetail([FromQuery] int orderId, [FromQuery] int productId, [FromBody] OrderDetailDto orderDetailCreate)
        {
            if (orderDetailCreate == null) { return BadRequest(ModelState); }

            var orderD = _orderDetailRepository.GetOrdersDetails()
                .Where(u => u.Id==orderDetailCreate.Id).FirstOrDefault();

            if (orderD != null)
            {
                ModelState.AddModelError("", "This Product already exists in your order");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderDMap = _mapper.Map<OrderDetail>(orderDetailCreate);
            orderDMap.Order = _orderRepository.GetOrder(orderId);
            orderDMap.Product = _productRepository.GetProduct(productId);

            if (!_orderDetailRepository.CreateOrderDetail(orderDMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }

        //__________________________UPDATE A ORDERDETAIL
        [HttpPut("{orderDetailId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOrderDetail(int orderDetailId, [FromBody] OrderDetailDto orderDetailUpdate)
        {
            if (orderDetailUpdate == null) { return BadRequest(ModelState); }

            if (orderDetailId != orderDetailUpdate.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_orderDetailRepository.OrderDetailExists(orderDetailId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var orderDetailMap = _mapper.Map<OrderDetail>(orderDetailUpdate);
            if (!_orderDetailRepository.UpdateOrderDetail(orderDetailMap))
            {
                ModelState.AddModelError("", "Something went wrong updating orderDetail");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        //__________________________DELETE A ORDERDETAIL
        [HttpDelete("{orderDetailId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOrder(int orderDetailId)
        {
            if (!_orderDetailRepository.OrderDetailExists(orderDetailId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderDetailToDelete = _orderDetailRepository.GetOrderDetail(orderDetailId);
            if (!_orderDetailRepository.DeleteOrderDetail(orderDetailToDelete))
                ModelState.AddModelError("", "Something went wrong deleting Order Detail");

            return NoContent();
        }
    }
}
