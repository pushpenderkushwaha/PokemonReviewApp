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
    public class OwnerController : ControllerBase
    {
        private readonly IOwner _owner;
        private readonly IMapper _mapper;
        private readonly IPokemon _pokemon;
        private readonly ICountry _country;

        public OwnerController(IOwner owner, IMapper mapper, IPokemon pokemon,ICountry country)
        {
            _owner = owner;
            _mapper = mapper;
            _pokemon = pokemon;
            _country = country; 

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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult createOwner([FromQuery] int countryId, [FromBody] OwnerDto ownerCreate)
        {
            if (ownerCreate == null)
            {
                return BadRequest();
            }

            var owner = _owner.GetOwners().Where(c => c.FirstName.Trim().ToUpper() == ownerCreate.FirstName.Trim().ToUpper())
                           .FirstOrDefault();

            if (owner != null)
            {
                ModelState.AddModelError("", "Owner Already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ownerMap = _mapper.Map<Owner>(ownerCreate);
            ownerMap.Country = _country.GetCountry(countryId);

            if (!_owner.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully completed");


        }

        [HttpPut("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner(int ownerId, [FromBody] OwnerDto updatedOwner)
        {
            if (updatedOwner == null)
                return BadRequest(ModelState);

            if (ownerId != updatedOwner.Id)
                return BadRequest(ModelState);

            if (!_owner.isOwnerExists(ownerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var ownerMap = _mapper.Map<Owner>(updatedOwner);

            if (!_owner.UpdateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Updated");
        }

        [HttpDelete("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int ownerId)
        {
            if (!_owner.isOwnerExists(ownerId))
            {
                return NotFound();
            }

            var ownerToDelete = _owner.GetOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_owner.DeleteOwner(ownerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return Ok("Succesfully Updated");
        }

    }
}
