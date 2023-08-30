using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewerRepository : IReviewer
    {
        private readonly DataContext _context;
        public ReviewerRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public ICollection<Review> GetReviewByReviewer(int id)
        {
            return _context.Reviews.Where(r=>r.Reviewer.Id==id).ToList();
        }

        public Reviewer GetReviewer(int id)
        {
            return _context.Reviewers.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.OrderBy(r => r.Id).ToList();
        }

        public bool ReviewerExists(int id)
        {
            return _context.Reviewers.Any(c=>c.Id == id);
        }
    }
}
