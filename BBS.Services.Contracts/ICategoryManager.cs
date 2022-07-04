using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface ICategoryManager
    {
        Category InsertCategory(Category category);
        Category? GetCategoryById(int categoryId);
        Category UpdateCategory(Category category);
        List<Category> GetCategories();
    }
}