using E_CommerceSystem.Models;

namespace E_CommerceSystem.Repositories
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
    }
}
