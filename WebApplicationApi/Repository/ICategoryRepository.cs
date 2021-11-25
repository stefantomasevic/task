using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationApi.Models;

namespace WebApplicationApi.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int Id);
        Category CreateCategory(Category category);
        Category EditCategory(Category category);
        void DeleteCategory(Category category);
    }
}
