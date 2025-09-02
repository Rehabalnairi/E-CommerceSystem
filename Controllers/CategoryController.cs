using AutoMapper;
using E_CommerceSystem.Models;
using E_CommerceSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _categoryService.GetAllCategories();
            var result = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null) return NotFound();
            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateDTO input)
        {
            var category = _mapper.Map<Category>(input);
            _categoryService.AddCategory(category);
            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryCreateDTO input)
        {
            var existing = _categoryService.GetCategoryById(id);
            if (existing == null) return NotFound();

            _mapper.Map(input, existing);
            _categoryService.UpdateCategory(existing);

            return Ok(_mapper.Map<CategoryDTO>(existing));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _categoryService.GetCategoryById(id);
            if (existing == null) return NotFound();

            _categoryService.DeleteCategory(id);
            return Ok();
        }
    }
}
