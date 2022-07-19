using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IGenericRepository<Category> _repositoryBase;

        public CategoryManager(IGenericRepository<Category> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }
        public List<Category> GetCategories()
        {
            return _repositoryBase.GetAll().ToList();
        }

        public Category? GetCategoryById(int categoryId)
        {
            return _repositoryBase
                .GetAll()
                .FirstOrDefault(c => c.Id == categoryId);
        }

        public Category InsertCategory(Category category)
        {
            var addedCategory = _repositoryBase.Insert(category);
            _repositoryBase.Save();
            return addedCategory;
        }

        public Category UpdateCategory(Category category)
        {
            var updatedCategory = _repositoryBase.Update(category);
            _repositoryBase.Save();
            return updatedCategory;
        }
    }
}
