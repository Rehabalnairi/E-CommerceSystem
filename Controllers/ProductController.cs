using AutoMapper; 
using E_CommerceSystem.Models;
using E_CommerceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        private readonly IMapper _mapper; //  AutoMapper

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // for athentication and authorization
        [Authorize(Roles = "admin")]
        [HttpPost("AddProduct")]
        public IActionResult AddNewProduct(ProductCreateDTO productInput)
        {
            if (productInput == null)
                return BadRequest("Product data is required.");

            // use AutoMapper to map DTO to Model
            var product = _mapper.Map<Product>(productInput);
            _productService.AddProduct(productInput);
            return Ok(product);
        }

        //for token 
        [Authorize(Roles = "admin")]
        [HttpPut("UpdateProduct/{productId}")]
        public IActionResult UpdateProduct(int productId, ProductDTO productInput)
        {
            if (productInput == null)
                return BadRequest("Product data is required.");

            var existingProduct = _productService.GetProductById(productId);
            if (existingProduct == null)
                return NotFound($"Product with ID {productId} not found.");

            // use AutoMapper to map DTO to existing Model
            _mapper.Map(productInput, existingProduct);

            _productService.UpdateProduct(existingProduct);
            return Ok(existingProduct);
        }

        [AllowAnonymous]
        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts(
            [FromQuery] string? name,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
                return BadRequest("PageNumber and PageSize must be greater than 0.");

            var products = _productService.GetAllProducts(pageNumber, pageSize, name, minPrice, maxPrice);

            if (products == null || !products.Any())
                return NotFound("No products found matching the given criteria.");
            //return paginated list with metadata
            return Ok(new
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = products.Count(),
                Data = _mapper.Map<IEnumerable<ProductDTO>>(products) 
            });
        }

        [AllowAnonymous]
        [HttpGet("GetProductByID/{ProductId}")]
        public IActionResult GetProductById(int ProductId)
        {
            var product = _productService.GetProductById(ProductId);
            if (product == null)
                return NotFound("No product found.");

            
            var productDto = _mapper.Map<ProductDTO>(product);
            return Ok(productDto);
        }

        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct([FromForm] ProductCreateDTO dto)
        {
            try
            {
                var product = _productService.AddProduct(dto);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
