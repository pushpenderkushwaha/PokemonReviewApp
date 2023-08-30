using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountry
    {
        private readonly DataContext _dataContext;
        public CountryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;   
        }
        public bool CoutryExists(int id)
        {
            return _dataContext.Countries.Any(c => c.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            _dataContext.Add(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _dataContext.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            var countries = _dataContext.Countries.OrderBy(c => c.Id).ToList();
            return countries;
        }

        public Country GetCountry(int id)
        {
            var country = _dataContext.Countries.Where(c=>c.Id==id).FirstOrDefault();
            return country;
        }

        public Country GetCountryByOwnerId(int id)
        {
            return _dataContext.Owners.Where(c => c.Id == id).Select(c=>c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerByCountryID(int countryId)
        {
            return _dataContext.Owners.Where(o=>o.Country.Id==countryId).ToList();
           
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _dataContext.Update(country);
            return Save();
        }
    }
}
