using SignalR1.Models;

namespace StoresManagement.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
    }
}
