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
    public class CountryController : ControllerBase
    {
        private readonly ICountry _country;
        private readonly IMapper _mapper;
        public CountryController(ICountry country, IMapper mapper)
        {
            _country = country;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type= typeof(IEnumerable<Country>))]
        public IActionResult getCountries() {
            var countries = _mapper.Map<List<CountryDto>>(_country.GetCountries());   
            return Ok(countries);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType(400)]
        public IActionResult getCountry(int id) {
            if(!_country.CoutryExists(id))
            {
                return NotFound();
            }

            var country = _mapper.Map<CountryDto>(_country.GetCountry(id));
            return Ok(country);
        }

        [HttpGet("owner/country/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType(400)]
        public IActionResult getCountyByOwnerId(int id)
        {
            var country = _mapper.Map<CountryDto>(_country.GetCountryByOwnerId(id));
            return Ok(country);
        }

        [HttpGet("country/owner/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]

        public IActionResult getOwnerByCountryId(int id)
        {
            if (!_country.CoutryExists(id))
            {
                return NotFound();
            }

            var owners = _mapper.Map<List<Owner>>(_country.GetOwnerByCountryID(id));

            return Ok(owners);

        }

    }
}
