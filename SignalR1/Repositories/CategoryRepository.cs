using SignalR1.Models;

namespace StoresManagement.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        NorthWindContext _context;
        public CategoryRepository(NorthWindContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
