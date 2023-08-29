namespace PokemonReviewApp.Models
{
    public class PokemonOwner
    {
        public int PokemonId { get; set; }
        public int OwnerID { get; set; }
        public Pokemon Pokemon { get; set; }
        public Owner Owner { get; set; }    

    }
}
