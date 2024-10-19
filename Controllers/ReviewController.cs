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
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository,
            IUserRepository userRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        //__________________________READ ALL REVIEWS
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        //__________________________READ A REVIEW
        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId)) { return NotFound(); }
            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        //__________________________CREATE A REVIEW
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int productId, [FromQuery] int userId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null) { return BadRequest(ModelState); }

            var review = _reviewRepository.GetReviews()
                .Where(u => u.Title.Trim().ToUpper()==reviewCreate.Title.Trim().ToUpper()
                || u.Text.Trim().ToUpper()==reviewCreate.Text.Trim().ToUpper()).FirstOrDefault();

            if (review != null)
            {
                ModelState.AddModelError("", "Review Already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewCreate);

            reviewMap.User = _userRepository.GetUser(userId);
            if (reviewMap.User == null) { ModelState.AddModelError("", "User don't found"); return StatusCode(404, ModelState); }

            reviewMap.Product = _productRepository.GetProduct(productId);
            if (reviewMap.Product == null) { ModelState.AddModelError("", "Product don't found"); return StatusCode(404, ModelState); }

            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }

        //__________________________UPDATE A REVIEW
        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] OrderDto reviewUpdate)
        {
            if (reviewUpdate == null) { return BadRequest(ModelState); }

            if (reviewId != reviewUpdate.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var reviewMap = _mapper.Map<Review>(reviewUpdate);
            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        //__________________________DELETE A REVIEW
        [HttpDelete("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewToDelete = _reviewRepository.GetReview(reviewId);
            if (!_reviewRepository.DeleteReview(reviewToDelete))
                ModelState.AddModelError("", "Something went wrong deleting review");

            return NoContent();
        }

    }
}
