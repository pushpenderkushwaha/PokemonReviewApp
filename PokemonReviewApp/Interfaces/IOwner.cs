using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IOwner
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int id);
        ICollection<Owner> GetOwnersByPokemonId(int pokeId);
        ICollection<Pokemon> GetPokemonByOwnerId(int ownerId);
        bool isOwnerExists(int ownerId);
        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool DeleteOwner(Owner owner);
        bool Save();
    }
}
