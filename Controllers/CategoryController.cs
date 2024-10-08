﻿using AutoMapper;
using EcommerceEjemploApi.Dto;
using EcommerceEjemploApi.Interfaces;
using EcommerceEjemploApi.Models;
using EcommerceEjemploApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceEjemploApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        //__________________________READ ALL CATEGORIES
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var catgeories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(catgeories);
        }

        //__________________________READ A CATEGORY
        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId)) { return NotFound(); }
            var catgeory = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(catgeory);
        }

        //__________________________CREATE A CATEGORY
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null) { return BadRequest(ModelState); }

            var categories = _categoryRepository.GetCategories()
                .Where(u => u.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper())
                .FirstOrDefault();
            if (categories != null)
            {
                ModelState.AddModelError("", "Category Already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }

        //__________________________UPDATE A CATEGORY
        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto categoryUpdate)
        {
            if (categoryUpdate == null) { return BadRequest(ModelState); }

            if (categoryId != categoryUpdate.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = _mapper.Map<Category>(categoryUpdate);
            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        //__________________________DELETE A PRODUCT
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryToDelete = _categoryRepository.GetCategory(categoryId);
            if (!_categoryRepository.DeleteCategory(categoryToDelete))
                ModelState.AddModelError("", "Something went wrong deleting category");

            return NoContent();
        }
    }
}
