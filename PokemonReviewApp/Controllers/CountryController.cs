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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult createCaoutry([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
            {
                return BadRequest();
            }

            var country = _country.GetCountries().Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.Trim().ToUpper())
                           .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country Already exists");
                return StatusCode(422, ModelState);
            } 

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = _mapper.Map<Country>(countryCreate);

            if (!_country.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully completed");


        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int countryId, [FromBody] CountryDto updatedCountry)
        {
            if (updatedCountry == null)
                return BadRequest(ModelState);

            if (countryId != updatedCountry.Id)
                return BadRequest(ModelState);

            if (!_country.CoutryExists(countryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var countryMap = _mapper.Map<Country>(updatedCountry);

            if (!_country.UpdateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        [HttpDelete("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCountry(int countryId)
        {
            if (!_country.CoutryExists(countryId))
            {
                return NotFound();
            }

            var countryToDelete = _country.GetCountry(countryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_country.DeleteCountry(countryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return Ok("Successfully Deleted");
        }


    }
}
