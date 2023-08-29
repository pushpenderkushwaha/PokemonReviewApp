using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICountry
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        ICollection<Owner> GetOwnerByCountryID(int countryId);
        Country GetCountryByOwnerId(int id);
        bool CoutryExists(int id);

    }
}
