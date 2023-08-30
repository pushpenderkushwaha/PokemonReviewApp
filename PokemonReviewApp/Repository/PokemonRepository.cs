using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemon
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext dataContext)
        {
            _context = dataContext;   
        }

        public bool CreatePokemon(int ownerID, int CateogryId, Pokemon pokemon)
        {
            var OwnerEntity = _context.Owners.Where(o => o.Id == ownerID).FirstOrDefault();
            var CategoryEntity = _context.Categories.Where(c => c.Id == CateogryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = OwnerEntity,
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = CategoryEntity,
                Pokemon = pokemon
            };

            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return Save();

        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(p => p.ID == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int id)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.ID == id).ToList();
            if (review.Count() <= 0)
            {
                return 0;
            }

            return ((decimal)review.Sum(r => r.Rating)) / review.Count;
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.ID).ToList();
        }

        public bool PokemonExits(int id)
        {
            return _context.Pokemons.Any(p => p.ID == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
