using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewer
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);
        ICollection<Review> GetReviewByReviewer(int id);
        bool ReviewerExists(int id);
        bool CreateReviewer(Reviewer reviewer);
        bool Save();
    }
}
