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
    public class OrderController : Controller    
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IUserRepository  userRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        //__________________________READ ALL ORDERS
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        public IActionResult GetOrders()
        {
            var orders = _mapper.Map<List<OrderDto>>(_orderRepository.GetOrders());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(orders);
        }


        //__________________________READ A ORDER
        [HttpGet("{orderId}")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(400)]
        public IActionResult GetOrder(int orderId)
        {
            if (!_orderRepository.OrderExists(orderId)) { return NotFound(); }
            var order = _mapper.Map<OrderDto>(_orderRepository.GetOrder(orderId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(order);
        }

        //__________________________CREATE A ORDER
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrder([FromQuery]int userId, [FromBody] OrderDto orderCreate)
        {
            if (orderCreate == null) { return BadRequest(ModelState); }

            var order = _orderRepository.GetOrders()
                .Where(u => u.Address == orderCreate.Address && u.TotalPrice == orderCreate.TotalPrice && u.Id==orderCreate.Id).FirstOrDefault();

            if (order != null)
            {
                ModelState.AddModelError("", "Order Already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(orderCreate);
            orderMap.User = _userRepository.GetUser(userId);

            if (!_orderRepository.CreateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }

        //__________________________UPDATE A ORDER
        [HttpPut("{orderId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOrder(int orderId, [FromBody] OrderDto orderUpdate)
        {
            if (orderUpdate == null) { return BadRequest(ModelState); }

            if (orderId != orderUpdate.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_orderRepository.OrderExists(orderId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var orderMap = _mapper.Map<Order>(orderUpdate);
            if (!_orderRepository.UpdateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong updating order");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        //__________________________DELETE A ORDER
        [HttpDelete("{orderId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOrder(int orderId)
        {
            if (!_orderRepository.OrderExists(orderId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderToDelete = _orderRepository.GetOrder(orderId);
            if (!_orderRepository.DeleteOrder(orderToDelete))
                ModelState.AddModelError("", "Something went wrong deleting order");

            return NoContent();
        }
    }
}
