using PokemonReviewApp.Models;

namespace PokemonReviewApp.Dto
{
    public class PokemonDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }
    }
}
