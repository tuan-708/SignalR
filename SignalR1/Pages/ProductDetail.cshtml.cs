using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Build.Evaluation;
using SignalR1.Hubs;
using SignalR1.Models;
using SignalR1.Repositories;
using StoresManagement.Repositories;
using System.ComponentModel.DataAnnotations;

namespace SignalR1.Pages
{
    public class ProductDetailModel : PageModel
    {
        IProductRepository _productRepository;
        ICategoryRepository _categoryRepository;
        ISuplierRepository _supplierRepository;
        IHubContext<ProductHub> _productHub;


        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Supplier> Suppliers { get; set; } = new List<Supplier>();
        [BindProperty]
        public Product ProductItem { get; set; } = new Product();

        public ProductDetailModel(IProductRepository productRepository, ICategoryRepository categoryRepository, ISuplierRepository supplierRepository, IHubContext<ProductHub> productHub)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _productHub = productHub;
        }


        public void OnGet(int Id)
        {

            ProductItem = _productRepository.GetProductById(Id);
            Suppliers = _supplierRepository.GetSupliers();
            Categories = _categoryRepository.GetCategories();
        }

        public void OnPost()
        {
            ProductItem.UnitsInStock = (short?)(ProductItem.UnitsInStock - 5);
            _productRepository.UpdateProduct(ProductItem);



            ProductItem = _productRepository.GetProductById(ProductItem.ProductId);
            Suppliers = _supplierRepository.GetSupliers();
            Categories = _categoryRepository.GetCategories();

        

            _productHub.Clients.All.SendAsync("UpdatedProduct", 5);
        }
    }
}
