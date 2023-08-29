using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _category;
        private readonly IMapper _mapper;
        public CategoryController(ICategory category, IMapper mapper)
        {
            _category = category;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult getCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_category.GetCategories());
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200,Type =  typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult getCategory(int id) {
          
            if (!_category.CategoryExists(id))
            {
                return NotFound();
            }

            var category = _mapper.Map<CategoryDto>(_category.GetCategory(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(category);
        }

        [HttpGet("pokemon/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]

        public IActionResult getPokemonByCategoryId(int id) {

            if (!_category.CategoryExists(id))
            {
                return NotFound();
            }

            var category = _mapper.Map<IEnumerable<PokemonDto>>(_category.GetPokemonsByCategoryId(id));

            return Ok(category);
        }
    }
}
