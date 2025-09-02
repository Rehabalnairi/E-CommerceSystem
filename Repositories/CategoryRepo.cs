using E_CommerceSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace E_CommerceSystem.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly List<Category> _categories = new(); // Replace with DbContext in EF Core

        public void AddCategory(Category category) => _categories.Add(category);

        public void DeleteCategory(int categoryId)
        {
            var category = GetCategoryById(categoryId);
            if (category != null) _categories.Remove(category);
        }

        public IEnumerable<Category> GetAllCategories() => _categories;

        public Category GetCategoryById(int categoryId) => _categories.FirstOrDefault(c => c.CategoryId == categoryId);

        public void UpdateCategory(Category category)
        {
            var existing = GetCategoryById(category.CategoryId);
            if (existing != null)
            {
                existing.Name = category.Name;
                existing.Description = category.Description;
            }
        }
    }
}
