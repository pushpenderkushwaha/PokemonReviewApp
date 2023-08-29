using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository:IPokemon
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext dataContext)
        {
            _context = dataContext;   
        }

        public  ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p=>p.ID).ToList();
        }

        Pokemon IPokemon.GetPokemon(int id)
        {
            return _context.Pokemons.Where(p => p.ID == id).FirstOrDefault();
        }

        Pokemon IPokemon.GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        decimal IPokemon.GetPokemonRating(int id)
        {
            var review  = _context.Reviews.Where(p=>p.Pokemon.ID == id).ToList();
            if (review.Count() <= 0)
            {
                return 0;
            }

            return ((decimal)review.Sum(r => r.Rating)) / review.Count;
        }

        bool IPokemon.PokemonExits(int id)
        {
            return _context.Pokemons.Any(p => p.ID == id);
        }
    }
}
