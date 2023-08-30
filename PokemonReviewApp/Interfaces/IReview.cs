using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReview
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewOfAPokemon(int id);
        bool isReviewExist(int id);
        bool CreateReview(Review review);
        bool Save();
    }
}
