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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var category = _mapper.Map<Category>(input);
                _categoryService.AddCategory(category);
                var result = _mapper.Map<CategoryDTO>(category);
                return CreatedAtAction(nameof(GetById), new { id = result.CategoryId }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating category: {ex.Message}");
            }
        }

            [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryCreateDTO input)
        {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existing = _categoryService.GetCategoryById(id);
            if (existing == null) return NotFound();

            try
            {
                _mapper.Map(input, existing);
                _categoryService.UpdateCategory(existing);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating category: {ex.Message}");
            }
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
