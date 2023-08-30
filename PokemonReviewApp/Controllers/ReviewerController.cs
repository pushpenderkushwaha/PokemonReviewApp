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
    public class ReviewerController : ControllerBase
    {
        private readonly IReviewer _reviewer;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewer reviewer, IMapper mapper)
        {
            _reviewer = reviewer;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult get()
        {
            var reviewer = _mapper.Map<List<ReviewerDto>>(_reviewer.GetReviewers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviewer);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        [ProducesResponseType(400)]
        public IActionResult get(int id)
        {
            if (!_reviewer.ReviewerExists(id))
            {
                return NotFound();
            }

            var reviwer = _mapper.Map<ReviewerDto>(_reviewer.GetReviewer(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviwer);
        }

        [HttpGet("{id}/review")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult getReviewbyReviewerId(int id)
        {
            if (!_reviewer.ReviewerExists(id))
            {
                return NotFound();
            }

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewer.GetReviewByReviewer(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
            
        }
    }
}
