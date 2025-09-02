using AutoMapper;
using E_CommerceSystem.Models;
using E_CommerceSystem.Repositories;
using System.Collections.Generic;

namespace E_CommerceSystem.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepo categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public Category AddCategory(Category category)
        {
            _categoryRepo.AddCategory(category);
            return category;
        }

        public void DeleteCategory(int categoryId) => _categoryRepo.DeleteCategory(categoryId);

        public IEnumerable<Category> GetAllCategories() => _categoryRepo.GetAllCategories();

        public Category GetCategoryById(int categoryId) => _categoryRepo.GetCategoryById(categoryId);

        public void UpdateCategory(Category category) => _categoryRepo.UpdateCategory(category);
    }
}
