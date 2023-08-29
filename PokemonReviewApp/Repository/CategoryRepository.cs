using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategory
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext dataContext)
        {
            _context = dataContext;   
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.Id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonsByCategoryId(int categoryId)
        {
            var category = _context.PokemonCategories.Where(p=>p.CategoryId == categoryId).Select(c=>c.Pokemon).ToList();
            return category;
        }
    }
}
