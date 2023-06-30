using SignalR1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace StoresManagement.Repositories
{
    public class ProductRepository:IProductRepository
    {
        NorthWindContext _context;
        public ProductRepository(NorthWindContext context)
        {
            _context = context;
        }

     
        public List<Product> GetProductsGetProductsByCategory(int? catId)
        {
            if (catId == null)
                return _context.Products.Include(p => p.Category).ToList();
            else
                return _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.CategoryId == catId)
                    .ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Include(p => p.Category).Include(p => p.Supplier).FirstOrDefault(p => p.ProductId == id);
        }

        public int AddNewProduct(Product product)
        {
            _context.Products.Add(product);
            return _context.SaveChanges();
        }

        public int UpdateProduct(Product product)
        {
            Product p = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (p != null)
            {
                p.ProductName = product.ProductName;
                p.CategoryId= product.CategoryId;
                p.SupplierId= product.SupplierId;
                p.UnitPrice = product.UnitPrice;
                p.QuantityPerUnit = product.QuantityPerUnit;
                p.Discontinued = product.Discontinued;
                p.UnitsInStock= product.UnitsInStock;
                p.UnitsOnOrder= product.UnitsOnOrder;
                p.ReorderLevel= product.ReorderLevel;
                return (_context.SaveChanges());
            }
            return 0;
        }

        public int DeleteProduct(int id)
        {
            Product p = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (p == null) return 0;
            else
            {
                List<OrderDetail> details= _context.OrderDetails.Where(p => p.ProductId == id).ToList();
                _context.OrderDetails.RemoveRange(details);
                _context.Products.Remove(p);
                return _context.SaveChanges();
            }
        }

        public List<Product> GetAllProduct()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }

        public List<Product> GetProductByIds(string pss)
        {
            List<Product> products = new List<Product>();
            if(pss != null)
            {
                var ps = pss.Split(".");
                foreach (var p in ps)
                {
                    var id = p.Split('-');
                    Product pro = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == Convert.ToInt32(id[0]));
                    pro.UnitsOnOrder = Convert.ToInt16(id[1]);
                    products.Add(pro);

                }

                return products;
            }
            return null;
        }
    }
}
