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
    public class PokemonController : ControllerBase
    {
        private readonly IPokemon _pokemon;
        private readonly IMapper _mapper;

        public PokemonController(IPokemon pokemon, IMapper mapper)
        {
            _pokemon = pokemon;   
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200,Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemon = _mapper.Map<List<PokemonDto>>(_pokemon.GetPokemons());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return pokemon == null ? NotFound() : Ok(pokemon);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemon(int id)
        {
            if (!_pokemon.PokemonExits(id))
            {
                return NotFound();
            }

            var pokemon = _mapper.Map<PokemonDto>(_pokemon.GetPokemon(id));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemon);

        }

        [HttpGet("{id}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int id)
        {
            if (!_pokemon.PokemonExits(id))
            {
                return NotFound();
            }

            var pokemonRating = _pokemon.GetPokemonRating(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemonRating);
        }
        

    }
}
