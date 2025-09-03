using E_CommerceSystem.Models;

namespace E_CommerceSystem.Services
{
    public interface IProductService
    {
        ProductDTO AddProduct(ProductCreateDTO dto);
        IEnumerable<Product> GetAllProducts(int pageNumber, int pageSize, string? name = null, decimal? minPrice = null, decimal? maxPrice = null);
        Product GetProductById(int pid);
        Product GetProductByName(string productName);
        void UpdateProduct(Product product);
    }
}