using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReview
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext dataContext)
        {
            _context = dataContext;   
        }

        public Review GetReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviewOfAPokemon(int id)
        {
            return _context.Reviews.Where(p=>p.Pokemon.ID == id).ToList();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.OrderBy(r=>r.Id).ToList();
        }

        public bool isReviewExist(int id)
        {
            return _context.Reviews.Any(r => r.Id == id);   
        }
    }
}
