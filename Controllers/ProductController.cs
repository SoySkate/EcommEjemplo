﻿using AutoMapper;
using EcommerceEjemploApi.Data;
using EcommerceEjemploApi.Dto;
using EcommerceEjemploApi.Interfaces;
using EcommerceEjemploApi.Models;
using EcommerceEjemploApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceEjemploApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        //__________________________READ ALL PRODUCTS
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(products);
        }

        //__________________________READ A PRODUCT
        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId)) { return NotFound(); }

            var product = _mapper.Map<ProductDto>(_productRepository.GetProduct(productId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        //__________________________CREATE A PRODUCT
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromQuery]int catId, [FromBody] ProductDto productCreate)
        {
            if (productCreate == null) { return BadRequest(ModelState); }

            var products = _productRepository.GetProducts()
                .Where(u => u.Name.Trim().ToUpper() == productCreate.Name.Trim().ToUpper())
                .FirstOrDefault();
            if (products != null)
            {
                ModelState.AddModelError("", "Product Already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productCreate);
            productMap.Category = _categoryRepository.GetCategory(catId);

            if (!_productRepository.CreateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }

        //__________________________UPDATE A PRODUCT
        [HttpPut("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProduct(int productId, [FromQuery] int catId, [FromBody] ProductDto productUpdate)
        {
            if (productUpdate == null) { return BadRequest(ModelState); }

            if (productId != productUpdate.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_productRepository.ProductExists(productId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var productMap = _mapper.Map<Product>(productUpdate);
            productMap.Category = _categoryRepository.GetCategory(catId);
            if (!_productRepository.UpdateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong updating product");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        //__________________________DELETE A PRODUCT
        [HttpDelete("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productToDelete = _productRepository.GetProduct(productId);
            if (!_productRepository.DeleteProduct(productToDelete))
                ModelState.AddModelError("", "Something went wrong deleting product");

            return NoContent();
        }


    }
}
