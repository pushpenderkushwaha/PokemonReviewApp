using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Diagnostics.Metrics;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwner
    {
        private readonly DataContext _context;
      
        public OwnerRepository(DataContext dataContext)
        {
            _context = dataContext;
     
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            return Save();
        }

        public Owner GetOwner(int id)
        {
            return _context.Owners.Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(o=>o.Id).ToList();
        }

        public ICollection<Owner> GetOwnersByPokemonId(int pokeId)
        {
            return _context.PokemonsOwner.Where(c => c.Pokemon.ID == pokeId).Select(c => c.Owner).ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwnerId(int ownerId)
        {
            return _context.PokemonsOwner.Where(c=>c.Owner.Id==ownerId).Select(c => c.Pokemon).ToList();
        }

        public bool isOwnerExists(int ownerId)
        {
            return _context.Owners.Any(c=>c.Id ==ownerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true :false;
        }

        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return Save(); 
        }
    }
}
