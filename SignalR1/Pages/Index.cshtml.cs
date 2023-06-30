using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Build.Evaluation;
using SignalR1.Hubs;
using SignalR1.Models;
using StoresManagement.Repositories;

namespace SignalR1.Pages
{
    public class IndexModel : PageModel
    {

        IProductRepository _productRepository;
        ICategoryRepository _categoryRepository;
        IHubContext<ProductHub> _productHub;

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Product> Products { get; set; } = new List<Product>();

        public IndexModel(IProductRepository productRepository, ICategoryRepository categoryRepository, IHubContext<ProductHub> productHub)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productHub= productHub;
        }

        public void OnGet()
        {
            LoadData();
        }

        private void LoadData()
        {
            Categories = _categoryRepository.GetCategories();

            Products = _productRepository.GetAllProduct();
        }

        public async void OnPost(int pid)
        {
           var a = _productRepository.DeleteProduct(pid);
       
            await _productHub.Clients.All.SendAsync("DeletedProduct", Convert.ToString(pid));

            LoadData();
        }
    }
}