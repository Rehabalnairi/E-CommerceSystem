using AutoMapper;
using E_CommerceSystem.Models;
using E_CommerceSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ProductService(IProductRepo productRepo, IMapper mapper, IWebHostEnvironment env)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _env = env;
        }

        public IEnumerable<Product> GetAllProducts(int pageNumber, int pageSize, string? name = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            // Base query
            var query = _productRepo.GetAllProducts().AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            // Pagination
            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
            .ToList();

        }

        public Product GetProductById(int pid)
        {
            var product = _productRepo.GetProductById(pid);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {pid} not found.");
            return product;
        }

        public ProductDTO AddProduct(ProductCreateDTO dto)
        {
            var product = _mapper.Map<Product>(dto);

            if (dto.ImageFile != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageFile.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                dto.ImageFile.CopyTo(stream);

                product.ImageUrl = "/uploads/" + fileName;
            }

            _productRepo.AddProduct(product);

            return _mapper.Map<ProductDTO>(product);
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _productRepo.GetProductById(product.PID);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {product.PID} not found.");

            _productRepo.UpdateProduct(product);
        }

        public Product GetProductByName(string productName)
        {
            var product = _productRepo.GetProductByName(productName);
            if (product == null)
                throw new KeyNotFoundException($"Product with Name {productName} not found.");
            return product;
        }
    }
}
