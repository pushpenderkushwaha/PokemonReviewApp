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
    public class ReviewController : ControllerBase
    {
        private readonly IReview _review;
        private readonly IMapper _mapper;
        private readonly IPokemon _pokemon;

        public ReviewController(IReview review,IMapper mapper,IPokemon pokemon )
        {
            _mapper = mapper;
            _review = review;   
            _pokemon = pokemon;
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Review>))]

        public IActionResult Get()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_review.GetReviews());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200,Type =typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]

        public IActionResult GetById(int id)
        {
            if (!_review.isReviewExist(id))
            {
                return NotFound();
            }

            var review = _mapper.Map<ReviewDto>(_review.GetReview(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(review);

        }

        [HttpGet("{id}/pokemon")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]

        public IActionResult GetReviewbyPokemonId(int id) {

            if (!_pokemon.PokemonExits(id))
            {
                return NotFound();
            }

            var review = _mapper.Map<List<ReviewDto>>(_review.GetReviewOfAPokemon(id));
            return Ok(review);

        }

    }
}
