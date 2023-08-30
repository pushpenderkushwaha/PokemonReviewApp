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
    public class OwnerController : ControllerBase
    {
        private readonly IOwner _owner;
        private readonly IMapper _mapper;
        private readonly IPokemon _pokemon;
        public OwnerController(IOwner owner, IMapper mapper, IPokemon pokemon)
        {
            _owner = owner;
            _mapper = mapper;
            _pokemon = pokemon;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult Get()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_owner.GetOwners());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return owners == null ? NotFound() : Ok(owners);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetById(int id)
        {
            if (!_owner.isOwnerExists(id))
            {
                return NotFound();
            }

            var owner = _mapper.Map<OwnerDto>(_owner.GetOwner(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(owner);

        }


        [HttpGet("{id}/pokemon")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByOwnerId(int id) {
            if (!_owner.isOwnerExists(id))
            {
                return NotFound();
            }

            var pokemons = _mapper.Map<List<PokemonDto>>(_owner.GetPokemonByOwnerId(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemons);
        }

        [HttpGet("pokemon/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnerByPokemonId(int id)
        {
            if (!_pokemon.PokemonExits(id))
            {
                return NotFound();
            }

            var owners = _mapper.Map<List<OwnerDto>>(_owner.GetOwnersByPokemonId(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

            return Ok(owners);

        }
    }
}
