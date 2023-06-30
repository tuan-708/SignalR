using SignalR1.Models;

namespace StoresManagement.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetProductsGetProductsByCategory(int? catId);
        List<Product> GetAllProduct();
        List<Product> GetProductByIds(string products);
        Product GetProductById(int id);
        int AddNewProduct(Product product);
        int UpdateProduct(Product product);
        int DeleteProduct(int id);
    }
}
