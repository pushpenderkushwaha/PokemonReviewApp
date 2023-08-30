using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReview _review;
        private readonly IMapper _mapper;
        private readonly IPokemon _pokemon;
        private readonly IReviewer _reviewer; 

        public ReviewController(IReview review,IMapper mapper,IPokemon pokemon,IReviewer reviewer )
        {
            _mapper = mapper;
            _review = review;   
            _pokemon = pokemon;
            _reviewer = reviewer;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult createReview([FromQuery] int pokemonId, [FromQuery] int reviewerID, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
            {
                return BadRequest();
            }

            var review = _review.GetReviews().Where(c => c.Title.Trim().ToUpper() == reviewCreate.Title.Trim().ToUpper())
                           .FirstOrDefault();

            if (review != null)
            {
                ModelState.AddModelError("", "Owner Already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewMap = _mapper.Map<Review>(reviewCreate);
            reviewMap.Pokemon = _pokemon.GetPokemon(pokemonId);
            reviewMap.Reviewer = _reviewer.GetReviewer(reviewerID);

            if (!_review.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully completed");


        }

    }
}
